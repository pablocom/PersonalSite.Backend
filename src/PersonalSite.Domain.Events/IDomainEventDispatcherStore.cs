using System.Collections.Concurrent;

namespace PersonalSite.Domain.Events;

public interface IDomainEventDispatcherStore
{
    void Push(DomainEventHandlerDispatcher domainEventHandlerDispatcher);
    void RunDomainEventHandlerDispatchers();
}

public class DomainEventDispatcherStore : IDomainEventDispatcherStore
{
    private readonly ConcurrentBag<DomainEventHandlerDispatcher> _domainEventHandlerDispatcher = new();

    public void Push(DomainEventHandlerDispatcher domainEventHandlerDispatcher)
    {
        _domainEventHandlerDispatcher.Add(domainEventHandlerDispatcher);
    }

    public void RunDomainEventHandlerDispatchers()
    {
        foreach (var dispatcher in _domainEventHandlerDispatcher)
            dispatcher.Run();
    }
}