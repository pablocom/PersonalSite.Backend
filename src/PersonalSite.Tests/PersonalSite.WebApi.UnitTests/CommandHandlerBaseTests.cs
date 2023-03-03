using MediatR;
using NSubstitute;
using PersonalSite.Application;
using PersonalSite.Application.CommandHandlers;
using PersonalSite.Persistence;
using Xunit;

namespace PersonalSite.WebApi.UnitTests;

public class CommandHandlerBaseTests
{
    private IUnitOfWork _unitOfWork;
    private CommandHandler<FakeCommand> _commandHandler;
    private readonly FakeCommand _command = new();

    public class FakeCommand : IRequest<Unit> { } // class intentionally public for mocking concerns

    public CommandHandlerBaseTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _commandHandler = Substitute.For<CommandHandler<FakeCommand>>(_unitOfWork);
    }

    [Fact]
    public async Task ProcessCommandWhenRequestIsHandled()
    {
        WhenRequestIsHandled();

        await _commandHandler.Received(1).Process(_command);
    }

    [Fact]
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

    [Fact]
    public void UnitOfWorkCommitsAfterProcessing()
    {
        WhenRequestIsHandled();

        _unitOfWork.Received(1).Commit();
    }

    private void WhenRequestIsHandled()
    {
        _commandHandler.Handle(_command).GetAwaiter().GetResult();
    }
}
