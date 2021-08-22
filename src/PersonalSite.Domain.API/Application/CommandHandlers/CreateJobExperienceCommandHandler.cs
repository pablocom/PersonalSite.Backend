﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Domain.API.Application.Commands;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Domain.Services;

namespace PersonalSite.Domain.API.Application.CommandHandlers
{
    public class CreateJobExperienceCommandHandler : IRequestHandler<CreateJobExperienceCommand, Unit>
    {
        private readonly IJobExperienceService service;

        public CreateJobExperienceCommandHandler(IJobExperienceService service)
        {
            this.service = service;
        }
        
        public async Task<Unit> Handle(CreateJobExperienceCommand command, CancellationToken cancellationToken)
        {
            service.CreateJobExperience(command.Company,
                command.Description,
                command.JobPeriodStart,
                command.JobPeriodEnd,
                command.TechStack);

            return await Unit.Task;
        }
    }
}