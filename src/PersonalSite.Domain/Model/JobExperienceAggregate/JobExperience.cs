using System;
using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Extensions;

namespace PersonalSite.Domain.Model.JobExperienceAggregate;

public class JobExperience : Entity, IAggregateRoot
{
    public string Company { get; protected set; }
    public string Description { get; protected set; }
    public JobPeriod JobPeriod { get; protected set; }
    public ICollection<string> TechStack { get; protected set; }

    protected JobExperience()
    { }

    public JobExperience(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, ICollection<string> techStack)
    {
        if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(description))
            throw new DomainException("Job experience company and description must have value");

        Company = company;
        Description = description;
        JobPeriod = new JobPeriod(jobPeriodStart, jobPeriodEnd);
        TechStack = techStack;

        DomainEvents.Add(new JobExperienceAdded(Company, Description, JobPeriod.Start, JobPeriod.End, TechStack.ToArray()));
    }
}
