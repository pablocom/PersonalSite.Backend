using MediatR;
using Moq;
using NUnit.Framework;
using PersonalSite.Domain;

namespace PersonalSite.WebApi.UnitTests;

[TestFixture]
public class MediatRDomainEventPublisherTests
{
    private Mock<IPublisher> _mediatRPublisherMock;
    private MediatRDomainEventPublisher _domainEventPublisher;

    [SetUp]
    public void SetUp()
    {
        _mediatRPublisherMock = new Mock<IPublisher>();
        _domainEventPublisher = new MediatRDomainEventPublisher(_mediatRPublisherMock.Object);
    }

    [Test]
    public async Task PublishesDomainEvents()
    {
        var events = new[]
        {
            new DummyEvent(1),
            new DummyEvent(2)
        };

        await _domainEventPublisher.Publish(events);

        _mediatRPublisherMock.Verify(
            x => x.Publish(It.Is<IDomainEvent>(ev => ev == events[0]), It.IsAny<CancellationToken>()), Times.Once);
        _mediatRPublisherMock.Verify(
            x => x.Publish(It.Is<IDomainEvent>(ev => ev == events[1]), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DoesNotPublishAnyEventIfEventListIsEmpty()
    {
        var events = Array.Empty<IDomainEvent>();

        await _domainEventPublisher.Publish(events);

        _mediatRPublisherMock.Verify(
            x => x.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()), Times.Never);
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
