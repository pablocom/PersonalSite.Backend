using System;
using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Extensions;

namespace PersonalSite.Domain.Model.JobExperienceAggregate;

public sealed class JobExperience : Entity, IAggregateRoot
{
    public string Company { get; private set; }
    public string Description { get; private set; }
    public JobPeriod JobPeriod { get; private set; }
    public ICollection<string> TechStack { get; private set; }

    protected JobExperience() { }

    public JobExperience(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, ICollection<string> techStack)
    {
        if (company.IsNullOrEmpty() || description.IsNullOrEmpty())
            throw new DomainException("Job experience company and description must have value");

        Company = company;
        Description = description;
        JobPeriod = new JobPeriod(jobPeriodStart, jobPeriodEnd);
        TechStack = techStack;

        DomainEvents.Add(new JobExperienceAdded(Company, Description, JobPeriod.Start, JobPeriod.End, TechStack.ToArray()));
    }
}
