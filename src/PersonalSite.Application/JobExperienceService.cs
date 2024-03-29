﻿using PersonalSite.Domain;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalSite.Application.Dtos;

namespace PersonalSite.Domain;

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
    private readonly IDomainEventPublisher _domainEventPublisher;

    public JobExperienceService(IJobExperienceRepository repository, IDomainEventPublisher domainEventPublisher)
    {
        _repository = repository;
        _domainEventPublisher = domainEventPublisher;
    }

    public async Task CreateJobExperience(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, string[] techStack)
    {
        var jobExperience = new JobExperience(company, description, jobPeriodStart, jobPeriodEnd, techStack);

        await _repository.Add(jobExperience);
        await _domainEventPublisher.Publish(jobExperience.DomainEvents);
    }

    public async Task<IEnumerable<JobExperienceDto>> GetJobExperiences()
    {
        var jobExperiences = await _repository.GetAllJobExperiences();
        return jobExperiences.Select(JobExperienceDto.From).ToArray();
    }
}
