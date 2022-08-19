using MediatR;
using PersonalSite.IoC;

namespace PersonalSite.Domain.Events;

public static class DomainEvents
{
    private static readonly AsyncLocal<IList<Delegate>?> t_currentHandlerActions = new();
    private static IList<Delegate> HandlerActions
    {
        get
        {
            if (t_currentHandlerActions.Value is null)
                t_currentHandlerActions.Value = new List<Delegate>();

            return t_currentHandlerActions.Value;
        }
    }
    private static readonly IList<Type> AsyncDomainEventHandlerTypes = new List<Type>();

    public static async Task Raise<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
    {
        InvokeRegisteredEventHandlerActions(domainEvent);
        await RaiseSyncDomainEventHandlers(domainEvent);
        SaveAggregateDomainEventHandlerDispatchers(domainEvent);
    }

    public static IDisposable Register<TDomainEvent>(Action<TDomainEvent> eventHandlerAction)
        where TDomainEvent : IDomainEvent
    {
        if (eventHandlerAction is null)
            throw new ArgumentNullException(nameof(eventHandlerAction));
        
        HandlerActions.Add(eventHandlerAction);
        return new DomainEventRegistrationRemover(() => HandlerActions.Remove(eventHandlerAction));
    }

    public static void RegisterAsyncHandler(Type domainEventHandlerType)
    {
        if (domainEventHandlerType is null)
            throw new ArgumentNullException(nameof(domainEventHandlerType));

        AsyncDomainEventHandlerTypes.Add(domainEventHandlerType);
    }

    private static void InvokeRegisteredEventHandlerActions<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
    {
        var noEventHandlerActionsRegistered = t_currentHandlerActions.Value is null;
        if (noEventHandlerActionsRegistered)
            return;

        foreach (var action in HandlerActions)
        {
            var typedAction = action as Action<TDomainEvent>;
            typedAction?.Invoke(domainEvent);
        }
    }

    private static async Task RaiseSyncDomainEventHandlers<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
    {
        var mediator = DependencyInjectionContainer.Current.GetRequiredService<IMediator>();
        await mediator.Publish(domainEvent);
    }

    private static void SaveAggregateDomainEventHandlerDispatchers<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
    {
        var domainEventHandlers = AsyncDomainEventHandlerTypes
            .Where(type => type.GetInterfaces().Where(i => i.IsGenericType)
                .Contains(typeof(IHandleDomainEventsAsynchronouslyAtTheEndOfTheCurrentScope<TDomainEvent>)))
            .ToArray();

        if (!domainEventHandlers.Any())
            return;

        var domainEventDispatcherStore = (IDomainEventDispatcherStore) DependencyInjectionContainer.Current.GetService(typeof(IDomainEventDispatcherStore))!;
        foreach (var handlerType in domainEventHandlers)
        {
            var handler = DependencyInjectionContainer.Current.GetRequiredService<IHandleDomainEventsAsynchronouslyAtTheEndOfTheCurrentScope<TDomainEvent>>();
            
            domainEventDispatcherStore.Add(new DomainEventHandlerDispatcher(async () =>
            {
                await handler.Handle(domainEvent);
            }));
        }
    }

    private sealed class DomainEventRegistrationRemover : IDisposable
    {
        private readonly Action _removalAction;

        public DomainEventRegistrationRemover(Action removalAction)
        {
            _removalAction = removalAction;
        }

        public void Dispose() => _removalAction();
    }
}
