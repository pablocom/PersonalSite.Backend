using MediatR;

namespace PersonalSite.Domain.Events;

public interface IHandleDomainEventsSynchronouslyInCurrentScope<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}