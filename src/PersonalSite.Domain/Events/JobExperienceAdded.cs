using System;
using System.Collections.Generic;

namespace PersonalSite.WebApi.Events;

public class JobExperienceAdded : IDomainEvent
{
    public string Company { get; set; }
    public string Description { get; set; }
    public DateTime JobPeriodStart { get; set; }
    public DateTime? JobPeriodEnd { get; set; }
    public IEnumerable<string> TechStack { get; set; }

    public JobExperienceAdded(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, IEnumerable<string> techStack)
    {
        Company = company;
        Description = description;
        JobPeriodStart = jobPeriodStart;
        JobPeriodEnd = jobPeriodEnd;
        TechStack = techStack;
    }
}
