using Microsoft.EntityFrameworkCore;
using PersonalSite.IntegrationEventsPublishWorker.EventPublisher;
using PersonalSite.Persistence;
using PersonalSite.Persistence.Events;

namespace PersonalSite.IntegrationEventsPublishWorker.UnitTests;

public class WhenReadingEventsFromDatabaseAndPublishingThemToServiceBus
{
    [Fact]
    public async Task NoEventIsPublishedIfNoEventsAreInRepository()
    {
        var messageBusPublisherMock = new Mock<IMessageBusPublisher>();
        var dbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());
        var integrationEventsPublisher = new IntegrationEventsPublisher(dbContext, messageBusPublisherMock.Object);

        await integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = dbContext.PersistableEvents.ToArray();
        messageBusPublisherMock.Verify(x => x.Publish(It.IsAny<PersistableEvent>()), Times.Never);
    }

    [Fact]
    public async Task PublishEvents()
    {
        var messageBusPublisherMock = new Mock<IMessageBusPublisher>();
        var dbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());
        var integrationEventsPublisher = new IntegrationEventsPublisher(dbContext, messageBusPublisherMock.Object);

        AssumeEventsAreStored(dbContext, new[]
        {
            new PersistableEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168021"), "fullName1", "serializedData1", new DateTime(1996, 9, 20)),
            new PersistableEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168022"), "fullName2", "serializedData2", new DateTime(1996, 9, 25)),
        });

        await integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = dbContext.PersistableEvents.ToArray();
        messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<PersistableEvent>(x => x.FullyQualifiedTypeName == "fullName1")), Times.Exactly(1));
        messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<PersistableEvent>(x => x.FullyQualifiedTypeName == "fullName2")), Times.Exactly(1));
    }

    [Fact]
    public async Task SkipsAlreadyProcessedEvents()
    {
        var messageBusPublisherMock = new Mock<IMessageBusPublisher>();
        var dbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());
        var integrationEventsPublisher = new IntegrationEventsPublisher(dbContext, messageBusPublisherMock.Object);

        var processedEvent = Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168021");
        AssumeEventsAreStored(dbContext, new[]
        {
            new PersistableEvent(processedEvent, "fullName1", "serializedData1", new DateTime(1996, 9, 20)),
            new PersistableEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168022"), "fullName2", "serializedData2", new DateTime(1996, 9, 25)),
        });
        AssumeEventWasProcessed(dbContext, processedEvent);

        await integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = dbContext.PersistableEvents.ToArray();
        messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<PersistableEvent>(x => x.FullyQualifiedTypeName == "fullName1")), Times.Never);
        messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<PersistableEvent>(x => x.FullyQualifiedTypeName == "fullName2")), Times.Exactly(1));
    }

    [Fact]
    public async Task MarksAsProcessedEventsAfterPublishingToMessageBus()
    {
        var messageBusPublisherMock = new Mock<IMessageBusPublisher>();
        var dbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());
        var integrationEventsPublisher = new IntegrationEventsPublisher(dbContext, messageBusPublisherMock.Object);

        AssumeEventsAreStored(dbContext, new[]
        {
            new PersistableEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168021"), "fullName1", "serializedData1", new DateTime(1996, 9, 20)),
            new PersistableEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168022"), "fullName2", "serializedData2", new DateTime(1996, 9, 25)),
        });

        await integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = dbContext.PersistableEvents.ToArray();
        events[0].IsProcessed.Should().BeTrue();
        events[1].IsProcessed.Should().BeTrue();
    }

    private void AssumeEventsAreStored(PersonalSiteDbContext dbContext, PersistableEvent[] events)
    {
        dbContext.PersistableEvents.AddRange(events);
        dbContext.SaveChanges();
    }

    private void AssumeEventWasProcessed(PersonalSiteDbContext dbContext, Guid eventId)
    {
        var @event = dbContext.PersistableEvents.First(x => x.Id == eventId);
        @event.MarkAsProcessed();
        dbContext.SaveChanges();
    }
}
