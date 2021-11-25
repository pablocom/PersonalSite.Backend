using PersonalSite.Application.Dtos;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalSite.Application;

/// <summary>
/// The application service layer represents the use cases and behavior of the application. Use cases are
/// implemented as application services that contain application logic to coordinate the fulfillment of
/// a use case by delegating to the domain and infrastructural layers.
/// </summary>
public interface IJobExperienceService
{
    void CreateJobExperience(JobExperienceCreationArgs args);
    IEnumerable<JobExperienceDto> GetJobExperiences();
}

public class JobExperienceService : IJobExperienceService
{
    private readonly IJobExperienceRepository repository;

    public JobExperienceService(IJobExperienceRepository repository)
    {
        this.repository = repository;
    }

    public void CreateJobExperience(JobExperienceCreationArgs args)
    {
        repository.Add(new JobExperience(args.Company, args.Description, args.JobPeriodStart, args.JobPeriodEnd, args.TechStack));
    }

    public IEnumerable<JobExperienceDto> GetJobExperiences()
    {
        var jobExperiences = repository.GetAllJobExperiences().ToArray();
        return jobExperiences.Select(JobExperienceDto.From).ToArray();
    }
}
