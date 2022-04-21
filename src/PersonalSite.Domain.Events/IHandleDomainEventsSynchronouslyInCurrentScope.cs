namespace PersonalSite.Domain.Events;

public interface IHandleDomainEventsSynchronouslyInCurrentScope<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent);
}