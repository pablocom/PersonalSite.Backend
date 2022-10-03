using MediatR;
using PersonalSite.Domain;

namespace PersonalSite.WebApi;

public class MediatRDomainEventPublisher : IDomainEventPublisher
{
    private readonly IPublisher _publisher;

    public MediatRDomainEventPublisher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Publish(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var @event in domainEvents)
        {
            await _publisher.Publish(@event, CancellationToken.None);
        }
    }
}
