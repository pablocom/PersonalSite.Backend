﻿using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.Events;
using PersonalSite.Persistence;
using PersonalSite.WebApi.CommandHandlers;

namespace PersonalSite.WebApi.UnitTests;

[TestFixture]
public class CommandHandlerBaseTests
{
    private IUnitOfWork _unitOfWork;
    private IDomainEventDispatcherStore _domainEventDispatcherStore;
    private CommandHandler<FakeCommand> _commandHandler;
    private readonly FakeCommand _command = new();

    public class FakeCommand : IRequest<Unit> { } // class intentionally public for mocking concerns

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _domainEventDispatcherStore = Substitute.For<IDomainEventDispatcherStore>();
        _commandHandler = Substitute.For<CommandHandler<FakeCommand>>(_unitOfWork, _domainEventDispatcherStore);
    }

    [Test]
    public async Task ProcessCommandWhenRequestIsHandled()
    {
        WhenRequestIsHandled();

        await _commandHandler.Received(1).Process(_command);
    }

    [Test]
    public void RollbackUnitOfWorkOnError()
    {
        _commandHandler
            .When(x => x.Process(_command))
            .Throw<InvalidOperationException>();

        try
        {
            WhenRequestIsHandled();
        }
        catch (Exception)
        {
            // intentionally ignored
        }

        _unitOfWork.Received(1).Rollback();
    }

    [Test]
    public void UnitOfWorkEndsAfterProcessing()
    {
        WhenRequestIsHandled();

        _unitOfWork.Received(1).Commit();
    }

    [Test]
    public void RunDomainEventHandlerDispatchersInStore()
    {
        WhenRequestIsHandled();

        _domainEventDispatcherStore.Received(1).RunDomainEventHandlerDispatchers();
    }

    [Test]
    public void DoesNotRunAggregateDispatchersIfUnitOfWorkFailsToCommit()
    {
        _unitOfWork.When(x => x.Commit()).Throw<Exception>();

        try
        {
            WhenRequestIsHandled();
        }
        catch (Exception)
        {
            // intentionally ignored
        }

        _domainEventDispatcherStore.DidNotReceive().RunDomainEventHandlerDispatchers();
    }

    private void WhenRequestIsHandled()
    {
        _commandHandler.Handle(_command).GetAwaiter().GetResult();
    }
}