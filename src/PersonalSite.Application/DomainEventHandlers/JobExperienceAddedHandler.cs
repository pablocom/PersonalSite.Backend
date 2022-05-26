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

    public async Task Handle(JobExperienceAdded domainEvent)
    {
        Console.WriteLine($"{DateTime.UtcNow:s} - Handling synchronously {nameof(JobExperienceAdded)}...");

        var jobExperiences = await _jobExperienceRepository.GetAllJobExperiences();
    }
}
