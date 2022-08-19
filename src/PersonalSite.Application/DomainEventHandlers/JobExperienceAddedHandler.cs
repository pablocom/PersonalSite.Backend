using PersonalSite.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalSite.Application.DomainEventHandlers;

public class JobExperienceAddedHandler : IHandleDomainEventsSynchronouslyInCurrentScope<JobExperienceAdded>
{
    private readonly IJobExperienceRepository _jobExperienceRepository;

    public JobExperienceAddedHandler(IJobExperienceRepository jobExperienceRepository)
    {
        _jobExperienceRepository = jobExperienceRepository;
    }

    public async Task Handle(JobExperienceAdded domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{DateTime.UtcNow:s} - Handling synchronously {nameof(JobExperienceAdded)}...");

        var jobExperiences = await _jobExperienceRepository.GetAllJobExperiences(cancellationToken);
    }
}
