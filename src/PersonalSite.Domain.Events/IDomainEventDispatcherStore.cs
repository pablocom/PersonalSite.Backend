using System.Collections.Concurrent;

namespace PersonalSite.Domain.Events;

public interface IDomainEventDispatcherStore
{
    void Add(DomainEventHandlerDispatcher domainEventHandlerDispatcher);
    Task RunDomainEventHandlerDispatchers();
}

public class DomainEventDispatcherStore : IDomainEventDispatcherStore
{
    private readonly ConcurrentBag<DomainEventHandlerDispatcher> _domainEventHandlerDispatcher = new();

    public void Add(DomainEventHandlerDispatcher domainEventHandlerDispatcher)
    {
        _domainEventHandlerDispatcher.Add(domainEventHandlerDispatcher);
    }

    public async Task RunDomainEventHandlerDispatchers()
    {
        foreach (var dispatcher in _domainEventHandlerDispatcher)
            await dispatcher.Run();
    }
}