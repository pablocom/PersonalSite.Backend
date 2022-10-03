namespace PersonalSite.IntegrationEventsPublishWorker.EventPublisher;

public class IntegrationEventsPublishHeartbeat : BackgroundService
{
    private readonly ILogger<IntegrationEventsPublishHeartbeat> _logger;
    private readonly IServiceProvider _serviceProvider;    
    private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromSeconds(4));

    public IntegrationEventsPublishHeartbeat(ILogger<IntegrationEventsPublishHeartbeat> logger, IServiceProvider serviceProvider )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (await _periodicTimer.WaitForNextTickAsync(ct) && !ct.IsCancellationRequested)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var integrationEventsPublisher = scope.ServiceProvider.GetRequiredService<IntegrationEventsPublisher>();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await integrationEventsPublisher.PublishIntegrationEventsToServiceBus();
        }
    }
}