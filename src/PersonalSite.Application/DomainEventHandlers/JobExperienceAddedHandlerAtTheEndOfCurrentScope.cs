using PersonalSite.Domain.Events;
using System;
using System.Threading.Tasks;

namespace PersonalSite.Application.DomainEventHandlers;

public class JobExperienceAddedHandlerAtTheEndOfCurrentScope : IHandleDomainEventsAsynchronouslyAtTheEndOfTheCurrentScope<JobExperienceAdded>
{
    private readonly IJobExperienceRepository jobExperienceRepository;

    public JobExperienceAddedHandlerAtTheEndOfCurrentScope(IJobExperienceRepository jobExperienceRepository)
    {
        this.jobExperienceRepository = jobExperienceRepository;
    }

    public Task Handle(JobExperienceAdded domainEvent)
    {
        Console.WriteLine($"{DateTime.UtcNow:s} - Publishing {nameof(JobExperienceAdded)} to message bus...");

        var jobExperiences = jobExperienceRepository.GetAllJobExperiences();
        return Task.CompletedTask;
    }
}
