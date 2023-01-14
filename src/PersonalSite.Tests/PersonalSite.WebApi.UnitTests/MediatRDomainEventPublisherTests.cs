using MediatR;
using NSubstitute;
using PersonalSite.Domain;
using Xunit;

namespace PersonalSite.WebApi.UnitTests;

public class MediatRDomainEventPublisherTests
{
    private IPublisher _mediatRPublisherMock;
    private MediatRDomainEventPublisher _domainEventPublisher;

    public MediatRDomainEventPublisherTests()
    {
        _mediatRPublisherMock = Substitute.For<IPublisher>();
        _domainEventPublisher = new MediatRDomainEventPublisher(_mediatRPublisherMock);
    }

    [Fact]
    public async Task PublishesDomainEvent()
    {
        var events = new[]
        {
            new DummyEvent(1),
            new DummyEvent(2)
        };

        await _domainEventPublisher.Publish(events);

        await _mediatRPublisherMock.Received(1).Publish(Arg.Is<IDomainEvent>(ev => ev == events[0]), Arg.Any<CancellationToken>());
        await _mediatRPublisherMock.Received(1).Publish(Arg.Is<IDomainEvent>(ev => ev == events[1]), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DoesNotPublishAnyEventIfEventListIsEmpty()
    {
        var events = Array.Empty<IDomainEvent>();

        await _domainEventPublisher.Publish(events);

        await _mediatRPublisherMock.Received(0).Publish(Arg.Any<IDomainEvent>(), Arg.Any<CancellationToken>());
    }

    private sealed class DummyEvent : IDomainEvent
    {
        public int Id { get; }

        public DummyEvent(int id)
        {
            Id = id;
        }
    }
}
