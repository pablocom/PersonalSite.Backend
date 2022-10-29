using Microsoft.EntityFrameworkCore;
using PersonalSite.Persistence;

namespace PersonalSite.IntegrationEventsPublishWorker.EventPublisher;

public sealed class IntegrationEventsPublisher
{
    private readonly PersonalSiteDbContext _dbContext;
    private readonly IMessageBus _messageBusPublisher;
    private readonly ILogger<IntegrationEventsPublisher> _logger;

    public IntegrationEventsPublisher(PersonalSiteDbContext dbContext, IMessageBus messageBusPublisher, ILogger<IntegrationEventsPublisher> logger)
    {
        _dbContext = dbContext;
        _messageBusPublisher = messageBusPublisher;
        _logger = logger;
    }

    public async Task PublishIntegrationEventsToServiceBus()
    {
        var integrationEvents = await _dbContext.IntegrationEvents.Where(x => x.IsPublished == false).ToArrayAsync();
        Console.WriteLine($"{DateTimeOffset.UtcNow} - Publishing {integrationEvents.Length} integration events...");

        foreach (var @event in integrationEvents)
        {
            _messageBusPublisher.Publish(@event);
            @event.MarkAsProcessed();
        }

        await _dbContext.SaveChangesAsync();
    }
}