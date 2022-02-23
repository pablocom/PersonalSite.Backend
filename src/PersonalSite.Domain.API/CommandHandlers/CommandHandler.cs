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
    private readonly IUnitOfWork unitOfWork;
    private readonly IDomainEventDispatcherStore domainEventDispatcherStore;

    protected CommandHandler(IUnitOfWork unitOfWork, IDomainEventDispatcherStore domainEventDispatcherStore)
    {
        this.unitOfWork = unitOfWork;
        this.domainEventDispatcherStore = domainEventDispatcherStore;
    }

    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await Process(command);
            unitOfWork.Commit();
            domainEventDispatcherStore.RunDomainEventHandlerDispatchers();
        }
        catch (Exception)
        {
            unitOfWork.Rollback();
            throw;
        }

        return Unit.Value;
    }

    public abstract Task Process(TCommand command);
}
