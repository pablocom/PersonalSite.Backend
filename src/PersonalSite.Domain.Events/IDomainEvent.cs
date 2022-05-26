using MediatR;

namespace PersonalSite.Domain.Events;

/// <summary>
/// Marker for domain events
/// </summary>
public interface IDomainEvent : INotification
{ }
