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

    public async Task PublishAsync(IEnumerable<IDomainEvent> domainEvent)
    {
        foreach (var domainEventItem in domainEvent)
            await _publisher.Publish(domainEventItem);
    }
}
