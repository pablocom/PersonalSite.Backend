using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain;

namespace PersonalSite.WebApi.UnitTests;

[TestFixture]
public class MediatRDomainEventPublisherTests
{
    private IPublisher _mediatRPublisherMock;
    private MediatRDomainEventPublisher _domainEventPublisher;

    [SetUp]
    public void SetUp()
    {
        _mediatRPublisherMock = Substitute.For<IPublisher>();
        _domainEventPublisher = new MediatRDomainEventPublisher(_mediatRPublisherMock);
    }

    [Test]
    public async Task PublishesDomainEvent()
    {
        var events = new[]
        {
            new DummyEvent(1),
            new DummyEvent(2)
        };

        await _domainEventPublisher.Publish(events);

        _mediatRPublisherMock.Received(1).Publish(Arg.Is<IDomainEvent>(ev => ev == events[0]), Arg.Any<CancellationToken>());
        _mediatRPublisherMock.Received(1).Publish(Arg.Is<IDomainEvent>(ev => ev == events[1]), Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task DoesNotPublishAnyEventIfEventListIsEmpty()
    {
        var events = Array.Empty<IDomainEvent>();

        await _domainEventPublisher.Publish(events);

        _mediatRPublisherMock.Received(0).Publish(Arg.Any<IDomainEvent>(), Arg.Any<CancellationToken>());
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
