namespace PersonalSite.Domain.Events;

public class DomainEventHandlerDispatcher
 
{
    private readonly Action dispatchAction;

    public DomainEventHandlerDispatcher(Action dispatchAction)
    {
        this.dispatchAction = dispatchAction;
    }

    public void Run() => dispatchAction();
}
