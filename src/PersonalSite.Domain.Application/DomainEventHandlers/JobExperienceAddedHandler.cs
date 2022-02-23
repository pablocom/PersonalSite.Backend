using PersonalSite.Domain.Events;
using System;

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

        Console.WriteLine($"{DateTime.UtcNow:s} - Handling synchronously {nameof(JobExperienceAdded)}...");
    }
}
