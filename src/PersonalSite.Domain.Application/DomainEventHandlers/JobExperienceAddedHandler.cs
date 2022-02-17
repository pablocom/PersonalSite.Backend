using PersonalSite.Domain.Events;

namespace PersonalSite.Application.DomainEventHandlers;

public class JobExperienceAddedHandler : IHandleDomainEventsSynchronouslyInCurrentScope<JobExperienceAdded>
{
    private readonly IJobExperienceRepository jobExperienceRepository;

    public JobExperienceAddedHandler(IJobExperienceRepository jobExperienceRepository)
    {
        this.jobExperienceRepository = jobExperienceRepository;
    }

    public void Handle(JobExperienceAdded domainEvent)
    {
        var jobExperiences = jobExperienceRepository.GetAllJobExperiences();


    }
}
