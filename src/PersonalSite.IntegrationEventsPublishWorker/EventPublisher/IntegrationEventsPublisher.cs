using Microsoft.EntityFrameworkCore;
using PersonalSite.Persistence;

namespace PersonalSite.IntegrationEventsPublishWorker.EventPublisher;

public sealed class IntegrationEventsPublisher
{
    private readonly PersonalSiteDbContext _dbContext;
    private readonly IMessageBusPublisher _messageBusPublisher;

    public IntegrationEventsPublisher(PersonalSiteDbContext dbContext, IMessageBusPublisher messageBusPublisher)
    {
        _dbContext = dbContext;
        _messageBusPublisher = messageBusPublisher;
    }

    public async Task PublishIntegrationEventsToServiceBus()
    {
        var events = await _dbContext.PersistableEvents.Where(x => x.IsProcessed == false).ToArrayAsync();
        Console.WriteLine($"{DateTimeOffset.UtcNow} - Publishing {events.Length} integration events...");

        foreach (var @event in events)
        {
            _messageBusPublisher.Publish(@event);
        }

        foreach (var @event in events)
        {
            @event.MarkAsProcessed();
        }

        await _dbContext.SaveChangesAsync();
    }
}