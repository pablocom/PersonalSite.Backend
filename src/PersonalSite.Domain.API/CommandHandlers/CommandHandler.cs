﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Persistence;

namespace PersonalSite.WebApi.CommandHandlers;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : IRequest<Unit>
{
    private readonly IUnitOfWork unitOfWork;

    protected CommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await Process(command);
            unitOfWork.Commit();
            // TODO: Dispatch domain events to queue
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
