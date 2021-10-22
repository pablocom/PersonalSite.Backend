using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using PersonalSite.Domain.API.Application.Commands;
using PersonalSite.Persistence;

namespace PersonalSite.API.UnitTests.Handlers.Base
{
    [TestFixture]
    public class CommandHandlerBaseTests
    {
        private IUnitOfWork unitOfWork;
        private CommandHandler<FakeCommand> commandHandler;
        private FakeCommand command = new();
        public class FakeCommand : IRequest<Unit> { }

        [SetUp]
        public void SetUp()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            commandHandler = Substitute.For<CommandHandler<FakeCommand>>(unitOfWork);
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
        public async Task UnitOfWorkEndsAfterProcessing()
        {
            WhenRequestIsHandled();
            
            await unitOfWork.Received(1).CommitAsync();
        }

        private void WhenRequestIsHandled()
        {
            commandHandler.Handle(command);
        }
    }
}