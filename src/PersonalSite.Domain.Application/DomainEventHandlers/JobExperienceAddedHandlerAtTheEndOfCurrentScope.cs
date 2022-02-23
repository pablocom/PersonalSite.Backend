using PersonalSite.Domain.Events;
using System;

namespace PersonalSite.Application.DomainEventHandlers;

public class JobExperienceAddedHandlerAtTheEndOfCurrentScope : IHandleDomainEventsAsynchronouslyAtTheEndOfTheCurrentScope<JobExperienceAdded>
{
    private readonly IJobExperienceRepository jobExperienceRepository;

    public JobExperienceAddedHandlerAtTheEndOfCurrentScope(IJobExperienceRepository jobExperienceRepository)
    {
        this.jobExperienceRepository = jobExperienceRepository;
    }

    public void Handle(JobExperienceAdded domainEvent)
    {
        var jobExperiences = jobExperienceRepository.GetAllJobExperiences();

        Console.WriteLine($"{DateTime.UtcNow:s} - Publishing {nameof(JobExperienceAdded)} to message bus...");
    }
}
