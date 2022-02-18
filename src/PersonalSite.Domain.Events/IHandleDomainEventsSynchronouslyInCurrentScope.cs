namespace PersonalSite.Domain.Events;

public interface IHandleDomainEventsSynchronouslyInCurrentScope<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    void Handle(TDomainEvent domainEvent);
}