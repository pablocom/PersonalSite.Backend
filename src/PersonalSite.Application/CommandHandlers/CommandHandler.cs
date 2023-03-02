using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Persistence;

namespace PersonalSite.Application.CommandHandlers;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : IRequest<Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    protected CommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await Process(command);
            _unitOfWork.Commit(); // TODO: here will the events be saved using the same transaction
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }

        return Unit.Value;
    }

    public abstract Task Process(TCommand command);
}
