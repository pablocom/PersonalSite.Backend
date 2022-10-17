using PersonalSite.Domain;

namespace PersonalSite.IntegrationEventsPublishWorker.EventPublisher;

/// <summary>
/// Dummy message bus for POC
/// </summary>
public interface IMessageBus
{
    void Publish<TMessage>(TMessage message) where TMessage : class;
}

public class DummyMessageBus : IMessageBus
{
    private readonly ILogger<DummyMessageBus> _logger;
    private readonly IClock _clock;

    public DummyMessageBus(ILogger<DummyMessageBus> logger, IClock clock)
    {
        _logger = logger;
        _clock = clock;
    }

    public void Publish<TMessage>(TMessage message) where TMessage : class
    {
        _logger.LogInformation("{Timestamp} - Publishing {MessageType} message...", 
            _clock.UtcNow, 
            message.GetType().Name);
    }
}
