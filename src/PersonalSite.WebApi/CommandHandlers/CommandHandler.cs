using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Domain.Events;
using PersonalSite.Persistence;

namespace PersonalSite.WebApi.CommandHandlers;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : IRequest<Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcherStore _domainEventDispatcherStore;

    protected CommandHandler(IUnitOfWork unitOfWork, IDomainEventDispatcherStore domainEventDispatcherStore)
    {
        _unitOfWork = unitOfWork;
        _domainEventDispatcherStore = domainEventDispatcherStore;
    }

    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await Process(command);
            _unitOfWork.Commit();
            await _domainEventDispatcherStore.RunDomainEventHandlerDispatchers();
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
