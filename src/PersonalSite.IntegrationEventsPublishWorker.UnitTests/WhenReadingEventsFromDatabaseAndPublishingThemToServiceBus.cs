using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalSite.IntegrationEventsPublishWorker.EventPublisher;
using PersonalSite.Persistence;
using PersonalSite.Persistence.Events;

namespace PersonalSite.IntegrationEventsPublishWorker.UnitTests;

public class WhenReadingEventsFromDatabaseAndPublishingThemToServiceBus
{
    private readonly IMessageBus _messageBusPublisherMock;
    private readonly FakeInMemoryPersonalSiteDbContext _dbContext;
    private readonly IntegrationEventsPublisher _integrationEventsPublisher;
    
    public WhenReadingEventsFromDatabaseAndPublishingThemToServiceBus()
    {
        _messageBusPublisherMock = Substitute.For<IMessageBus>();
        _dbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());

        _integrationEventsPublisher = new IntegrationEventsPublisher(
            _dbContext,
            _messageBusPublisherMock,
            Substitute.For<ILogger<IntegrationEventsPublisher>>());
    }

    [Fact]
    public async Task NoEventIsPublishedIfNoEventsAreInRepository()
    {
        await _integrationEventsPublisher.PublishIntegrationEventsToServiceBus();

        _messageBusPublisherMock.DidNotReceive().Publish(Arg.Any<IntegrationEvent>());
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

        _messageBusPublisherMock.Received(1).Publish(Arg.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName1"));
        _messageBusPublisherMock.Received(1).Publish(Arg.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName2"));
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

        _messageBusPublisherMock.Received(0).Publish(Arg.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName1"));
        _messageBusPublisherMock.Received(1).Publish(Arg.Is<IntegrationEvent>(x => x.FullyQualifiedTypeName == "fullName2"));
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
