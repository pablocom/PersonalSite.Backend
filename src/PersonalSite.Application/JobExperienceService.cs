using PersonalSite.Application.Dtos;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalSite.Application;

/// <summary>
/// The application service layer represents the use cases and behavior of the application. Use cases are
/// implemented as application services that contain application logic to coordinate the fulfillment of
/// a use case by delegating to the domain and infrastructural layers.
/// </summary>
public interface IJobExperienceService
{
    Task CreateJobExperience(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, string[] techStack);
    IEnumerable<JobExperienceDto> GetJobExperiences();
}

public class JobExperienceService : IJobExperienceService
{
    private readonly IJobExperienceRepository repository;

    public JobExperienceService(IJobExperienceRepository repository)
    {
        this.repository = repository;
    }

    public async Task CreateJobExperience(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, string[] techStack)
    {
        var jobExperience = await JobExperience.Create(company, description, jobPeriodStart, jobPeriodEnd, techStack);
        repository.Add(jobExperience);
    }

    public IEnumerable<JobExperienceDto> GetJobExperiences()
    {
        var jobExperiences = repository.GetAllJobExperiences().ToArray();
        return jobExperiences.Select(JobExperienceDto.From).ToArray();
    }
}
