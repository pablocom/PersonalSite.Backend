using PersonalSite.Domain.Events;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace PersonalSite.Domain.Model.JobExperienceAggregate;

public class JobExperience : Entity, IAggregateRoot
{
    public string Company { get; protected set; }
    public string Description { get; protected set; }
    public JobPeriod JobPeriod { get; protected set; }
    public ICollection<string> TechStack { get; protected set; }

    protected JobExperience()
    { }

    public JobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, ICollection<string> techStack)
    {
        if (company.IsNullOrEmpty() || description.IsNullOrEmpty())
            throw new DomainException("Job experience company and description must have value");

        Company = company;
        Description = description;
        JobPeriod = new JobPeriod(jobPeriodStart, jobPeriodEnd);
        TechStack = techStack;

        DomainEvents.Raise(new JobExperienceCreated(Company, Description, JobPeriod.Start, JobPeriod.End, TechStack));
    }
}
