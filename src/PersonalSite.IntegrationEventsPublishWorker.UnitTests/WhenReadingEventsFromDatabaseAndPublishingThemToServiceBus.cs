using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalSite.IntegrationEventsPublishWorker.EventPublisher;
using PersonalSite.Persistence;
using PersonalSite.Persistence.Events;

namespace PersonalSite.IntegrationEventsPublishWorker.UnitTests;

public class WhenReadingEventsFromDatabaseAndPublishingThemToServiceBus
{
    private readonly Mock<IMessageBus> _messageBusPublisherMock;
    private readonly FakeInMemoryPersonalSiteDbContext _dbContext;
    private readonly IntegrationEventsPublisher _integrationEventsPublisher;
    
    public WhenReadingEventsFromDatabaseAndPublishingThemToServiceBus()
    {
        _messageBusPublisherMock = new Mock<IMessageBus>();
        _dbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());

        _integrationEventsPublisher = new IntegrationEventsPublisher(
            _dbContext,
            _messageBusPublisherMock.Object,
            Mock.Of<ILogger<IntegrationEventsPublisher>>());
    }

    [Fact]
    public async Task NoEventIsPublishedIfNoEventsAreInRepository()
    {
        await _integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = _dbContext.IntegrationEvents.ToArray();
        _messageBusPublisherMock.Verify(x => x.Publish(It.IsAny<IntegrationEvent>()), Times.Never);
    }

    [Fact]
    public async Task PublishEvents()
    {
        AssumeEventsAreStored(new[]
        {
            new IntegrationEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168021"), "fullName1", "serializedData1", new DateTime(1996, 9, 20)),
            new IntegrationEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168022"), "fullName2", "serializedData2", new DateTime(1996, 9, 25)),
        });

        await _integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = _dbContext.IntegrationEvents.ToArray();
        _messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName1")), Times.Exactly(1));
        _messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName2")), Times.Exactly(1));
    }

    [Fact]
    public async Task SkipsAlreadyProcessedEvents()
    {
        var processedEvent = Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168021");
        AssumeEventsAreStored(new[]
        {
            new IntegrationEvent(processedEvent, "fullName1", "serializedData1", new DateTime(1996, 9, 20)),
            new IntegrationEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168022"), "fullName2", "serializedData2", new DateTime(1996, 9, 25)),
        });
        AssumeEventWasPublished(processedEvent);

        await _integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = _dbContext.IntegrationEvents.ToArray();
        _messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName1")), Times.Never);
        _messageBusPublisherMock
            .Verify(x => x.Publish(It.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName2")), Times.Exactly(1));
    }

    [Fact]
    public async Task MarksAsPublishedEventsAfterPublishingToMessageBus()
    {
        

        AssumeEventsAreStored(new[]
        {
            new IntegrationEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168021"), "fullName1", "serializedData1", new DateTime(1996, 9, 20)),
            new IntegrationEvent(Guid.Parse("A01B92E2-1A73-4065-9CD5-B49C0A168022"), "fullName2", "serializedData2", new DateTime(1996, 9, 25)),
        });

        await _integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        var events = _dbContext.IntegrationEvents.ToArray();
        events[0].IsPublished.Should().BeTrue();
        events[1].IsPublished.Should().BeTrue();
    }

    private void AssumeEventsAreStored(IntegrationEvent[] events)
    {
        _dbContext.IntegrationEvents.AddRange(events);
        _dbContext.SaveChanges();
    }

    private void AssumeEventWasPublished(Guid eventId)
    {
        var @event = _dbContext.IntegrationEvents.First(x => x.Id == eventId);
        @event.MarkAsProcessed();
        _dbContext.SaveChanges();
    }
}
