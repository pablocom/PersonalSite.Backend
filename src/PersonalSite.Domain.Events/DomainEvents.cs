using PersonalSite.IoC;

namespace PersonalSite.Domain.Events;

public static class DomainEvents
{
    private static readonly IList<Type> SyncDomainEventHandlerTypes = new List<Type>();
    private static readonly IList<Type> AsyncDomainEventHandlerTypes = new List<Type>();
    private static readonly SemaphoreSlim SemaphoreSlim = new(1, 1);
    
    [Obsolete]
    public static void Init()
    {
        
    }

    public static void Raise<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
    {   
        RaiseSyncDomainEventHandlers(domainEvent);
    }

    private static void RaiseSyncDomainEventHandlers<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
    {
        var syncDomainEventHandlers = new Queue<Type>(
            TakeHandlersOfType<IHandleDomainEventsSynchronouslyInCurrentScope<TDomainEvent>>(SyncDomainEventHandlerTypes)
        );

        while (syncDomainEventHandlers.Any())
        {
            var domainEventHandlerType = syncDomainEventHandlers.Dequeue();

            try
            {
                var handler = DependencyInjectionContainer.Current.GetService(domainEventHandlerType) as IHandleDomainEventsSynchronouslyInCurrentScope<TDomainEvent>;
                if (handler is null)
                {
                    throw new InvalidOperationException($"Cannot resolve domain event handler for {domainEventHandlerType.Name}");
                }

                handler.Handle(domainEvent);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception: {ex} while executing synchronous handler {domainEventHandlerType.Name}");
                foreach(var handlerType in syncDomainEventHandlers)
                {
                    Console.Error.WriteLine($"Skipping execution of a handler " + handlerType.Name);
                }
                throw;
            }
        }
    }

    public static void RegisterSyncHandler(Type domainEventHandlerType)
    {
        if (domainEventHandlerType is null)
            throw new ArgumentNullException(nameof(domainEventHandlerType));

        SyncDomainEventHandlerTypes.Add(domainEventHandlerType);
    }

    private static IEnumerable<Type> TakeHandlersOfType<TDomainEventHandler>(IEnumerable<Type> allHandlerTypes)
    {
        return allHandlerTypes.Where(type => type.GetInterfaces().Where(i => i.IsGenericType).Contains(typeof(TDomainEventHandler)))
            .ToArray();
    }
}