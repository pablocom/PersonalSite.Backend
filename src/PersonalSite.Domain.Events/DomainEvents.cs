namespace PersonalSite.Domain.Events;

public static class DomainEvents
{
    private static readonly IList<Type> SyncDomainEventHandlerTypes = new List<Type>();
    private static readonly IList<Type> AsyncDomainEventHandlerTypes = new List<Type>();
    private static readonly SemaphoreSlim SemaphoreSlim = new(1, 1);
    
    public static void Init()
    {
        
    }

    public static void Register<TDomainEvent>(Action<TDomainEvent> eventHandler)
    {
    }

    public static async Task Raise<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
    {
        await SemaphoreSlim.WaitAsync().ConfigureAwait(false);
        
        await RaiseSyncDomainEventHandlers(domainEvent).ConfigureAwait(false);
    }

    private static Task RaiseSyncDomainEventHandlers<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
    {
        var syncDomainEventHandlers = new Queue<Type>();
    }
    
    private static IEnumerable<Type> TakeHandlers<TDomainEventHandler>(IEnumerable<Type> allHandlerTypes)
    {
        return allHandlerTypes.Where(type => type.GetInterfaces().Where(i => i.IsGenericType).Contains(typeof(TDomainEventHandler)))
            .ToArray();
    }
}