using System.Threading.Tasks;
using PersonalSite.Application;
using PersonalSite.Domain.Events;
using PersonalSite.Persistence;
using PersonalSite.WebApi.Commands;

namespace PersonalSite.WebApi.CommandHandlers;

public class CreateJobExperienceCommandHandler : CommandHandler<CreateJobExperienceCommand>
{
    private readonly IJobExperienceService service;

    public CreateJobExperienceCommandHandler(IJobExperienceService service, IUnitOfWork unitOfWork, IDomainEventDispatcherStore dispatcherStore) 
        : base(unitOfWork, dispatcherStore)
    {
        this.service = service;
    }

    public override Task Process(CreateJobExperienceCommand command)
    {
        service.CreateJobExperience(
            command.Company,
            command.Description,
            command.JobPeriodStart,
            command.JobPeriodEnd,
            command.TechStack);

        return Task.CompletedTask;
    }
}
