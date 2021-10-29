﻿using System.Threading.Tasks;
using PersonalSite.Domain.API.Commands;
using PersonalSite.Domain.Application;
using PersonalSite.Persistence;

namespace PersonalSite.Domain.API.CommandHandlers
{
    public class CreateJobExperienceCommandHandler : CommandHandler<CreateJobExperienceCommand>
    {
        private readonly IJobExperienceService service;

        public CreateJobExperienceCommandHandler(IJobExperienceService service, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.service = service;
        }
        
        public override Task Process(CreateJobExperienceCommand command)
        {
            service.CreateJobExperience(command.Company,
                command.Description,
                command.JobPeriodStart,
                command.JobPeriodEnd,
                command.TechStack);
            
            return Task.CompletedTask;
        }
    }
}