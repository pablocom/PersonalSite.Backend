namespace PersonalSite.Domain.Events;

public class DomainEventHandlerDispatcher
 
{
    private readonly Func<Task> _dispatchAction;

    public DomainEventHandlerDispatcher(Func<Task> dispatchAction)
    {
        _dispatchAction = dispatchAction;
    }

    public async Task Run()
    {
        await _dispatchAction().ConfigureAwait(false);
    }
}
