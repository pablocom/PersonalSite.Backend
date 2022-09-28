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
    Task<IEnumerable<JobExperienceDto>> GetJobExperiences();
}

public class JobExperienceService : IJobExperienceService
{
    private readonly IJobExperienceRepository _repository;

    public JobExperienceService(IJobExperienceRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateJobExperience(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, string[] techStack)
    {
        var jobExperience = new JobExperience(company, description, jobPeriodStart, jobPeriodEnd, techStack);
        await _repository.Add(jobExperience);
    }

    public async Task<IEnumerable<JobExperienceDto>> GetJobExperiences()
    {
        var jobExperiences = await _repository.GetAllJobExperiences();
        return jobExperiences.Select(JobExperienceDto.From).ToArray();
    }
}
