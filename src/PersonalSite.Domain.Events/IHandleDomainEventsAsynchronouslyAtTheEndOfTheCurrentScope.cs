namespace PersonalSite.Domain.Events;

public interface IHandleDomainEventsAsynchronouslyAtTheEndOfTheCurrentScope<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent);
}