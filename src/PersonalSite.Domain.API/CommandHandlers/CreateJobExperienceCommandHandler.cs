using PersonalSite.Application;
using PersonalSite.Application.Dtos;
using PersonalSite.Persistence;
using PersonalSite.WebApi.Commands;
using System.Threading.Tasks;

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
        var args = new JobExperienceCreationArgs(command.Company, command.Description, command.JobPeriodStart, command.JobPeriodEnd, command.TechStack);
        service.CreateJobExperience(args);

        return Task.CompletedTask;
    }
}
