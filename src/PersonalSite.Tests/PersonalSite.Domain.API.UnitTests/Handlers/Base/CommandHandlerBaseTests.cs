using System;
using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.Events;
using PersonalSite.Persistence;
using PersonalSite.WebApi.CommandHandlers;

namespace PersonalSite.API.UnitTests.Handlers.Base;

[TestFixture]
public class CommandHandlerBaseTests
{
    private IUnitOfWork unitOfWork;
    private IDomainEventDispatcherStore domainEventDispatcherStore;
    private CommandHandler<FakeCommand> commandHandler;
    private FakeCommand command = new();

    // class intentionally public for mocking concerns
    public class FakeCommand : IRequest<Unit> { }

    [SetUp]
    public void SetUp()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        domainEventDispatcherStore = Substitute.For<IDomainEventDispatcherStore>();
        commandHandler = Substitute.For<CommandHandler<FakeCommand>>(unitOfWork, domainEventDispatcherStore);
    }

    [Test]
    public async Task ProcessCommandWhenRequestIsHandled()
    {
        WhenRequestIsHandled();

        await commandHandler.Received(1).Process(command);
    }

    [Test]
    public void RollbackUnitOfWorkOnError()
    {
        commandHandler
            .When(x => x.Process(command))
            .Throw<InvalidOperationException>();

        WhenRequestIsHandled();

        unitOfWork.Received(1).Rollback();
    }

    [Test]
    public void UnitOfWorkEndsAfterProcessing()
    {
        WhenRequestIsHandled();

        unitOfWork.Received(1).Commit();
    }

    [Test]
    public void RunDomainEventHandlerDispatchersInStore()
    {
        WhenRequestIsHandled();

        domainEventDispatcherStore.Received(1).RunDomainEventHandlerDispatchers();
    }

    [Test]
    public void DoesNotRunAggregateDispatchersIfUnitOfWorkFailsToCommit()
    {
        unitOfWork.When(x => x.Commit()).Throw<Exception>();

        WhenRequestIsHandled();

        domainEventDispatcherStore.DidNotReceive().RunDomainEventHandlerDispatchers();
    }

    private void WhenRequestIsHandled()
    {
        commandHandler.Handle(command);
    }
}
