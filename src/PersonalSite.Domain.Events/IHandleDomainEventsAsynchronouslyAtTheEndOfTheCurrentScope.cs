namespace PersonalSite.Domain.Events;

public interface IHandleDomainEventsAsynchronouslyAtTheEndOfTheCurrentScope<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    void Handle(TDomainEvent domainEvent);
}