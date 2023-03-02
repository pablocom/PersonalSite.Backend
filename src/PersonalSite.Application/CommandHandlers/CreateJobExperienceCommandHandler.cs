using System.Threading.Tasks;
using PersonalSite.Application.Commands;
using PersonalSite.Persistence;

namespace PersonalSite.Application.CommandHandlers;

public class CreateJobExperienceCommandHandler : CommandHandler<CreateJobExperienceCommand>
{
    private readonly IJobExperienceService _service;

    public CreateJobExperienceCommandHandler(IJobExperienceService service, IUnitOfWork unitOfWork) 
        : base(unitOfWork)
    {
        _service = service;
    }

    public override async Task Process(CreateJobExperienceCommand command)
    {
        await _service.CreateJobExperience(
            command.Company,
            command.Description,
            command.JobPeriodStart,
            command.JobPeriodEnd,
            command.TechStack);
    }
}
