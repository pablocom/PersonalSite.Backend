using System;
using System.Collections.Generic;

namespace PersonalSite.Domain.Events;

public class JobExperienceCreated : IDomainEvent
{
    public string Company { get; set; }
    public string Description { get; set; }
    public DateTime JobPeriodStart { get; set; }
    public DateTime? JobPeriodEnd { get; set; }
    public IEnumerable<string> TechStack { get; set; }

    public JobExperienceCreated(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, IEnumerable<string> techStack)
    {
        Company = company;
        Description = description;
        JobPeriodStart = jobPeriodStart;
        JobPeriodEnd = jobPeriodEnd;
        TechStack = techStack;
    }
}