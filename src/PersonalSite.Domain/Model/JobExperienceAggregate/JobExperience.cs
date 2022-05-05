using PersonalSite.Domain.Events;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Domain.Model.JobExperienceAggregate;

public class JobExperience : Entity<int>, IAggregateRoot
{
    public string Company { get; protected set; }
    public string Description { get; protected set; }
    public JobPeriod JobPeriod { get; protected set; }
    public ICollection<string> TechStack { get; protected set; }

    protected JobExperience()
    { }

    public JobExperience(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, ICollection<string> techStack)
    {
        if (company.IsNullOrEmpty() || description.IsNullOrEmpty())
            throw new DomainException("Job experience company and description must have value");

        Company = company;
        Description = description;
        JobPeriod = new JobPeriod(jobPeriodStart, jobPeriodEnd);
        TechStack = techStack;
    }

    public static async Task<JobExperience> Create(string company, string description, DateOnly jobPeriodStart,
        DateOnly? jobPeriodEnd, ICollection<string> techStack)
    {
        var jobExperience = new JobExperience(company, description, jobPeriodStart, jobPeriodEnd, techStack);
        await DomainEvents.Raise(new JobExperienceAdded(company, description, jobPeriodStart, jobPeriodEnd, techStack));
        return jobExperience;
    }
}
