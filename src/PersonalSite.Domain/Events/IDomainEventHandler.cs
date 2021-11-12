using PersonalSite.Domain.Events;

namespace PersonalSite.Domain;

public interface IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    void Handle(TDomainEvent domainEvent);
}
