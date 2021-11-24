using System.Threading.Tasks;
using PersonalSite.Application;
using PersonalSite.Persistence;
using PersonalSite.WebApi.Commands;

namespace PersonalSite.WebApi.CommandHandlers;

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
