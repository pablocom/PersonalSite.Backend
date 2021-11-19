namespace PersonalSite.WebApi.Events;

public interface IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    void Handle(TDomainEvent domainEvent);
}
