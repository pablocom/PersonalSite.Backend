using PersonalSite.Domain.Events;
using System;
using System.Threading.Tasks;

namespace PersonalSite.Application.DomainEventHandlers;

public class JobExperienceAddedHandler : IHandleDomainEventsSynchronouslyInCurrentScope<JobExperienceAdded>
{
    private readonly IJobExperienceRepository _jobExperienceRepository;

    public JobExperienceAddedHandler(IJobExperienceRepository jobExperienceRepository)
    {
        _jobExperienceRepository = jobExperienceRepository;
    }

    public Task Handle(JobExperienceAdded domainEvent)
    {
        Console.WriteLine($"{DateTime.UtcNow:s} - Handling synchronously {nameof(JobExperienceAdded)}...");

        var jobExperiences = _jobExperienceRepository.GetAllJobExperiences();
        return Task.CompletedTask;
    }
}
