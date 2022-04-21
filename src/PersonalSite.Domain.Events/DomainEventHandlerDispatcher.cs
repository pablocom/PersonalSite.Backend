namespace PersonalSite.Domain.Events;

public class DomainEventHandlerDispatcher
 
{
    private readonly Func<Task> dispatchAction;

    public DomainEventHandlerDispatcher(Func<Task> dispatchAction)
    {
        this.dispatchAction = dispatchAction;
    }

    public async Task Run()
    {
        await dispatchAction().ConfigureAwait(false);
    }
}
