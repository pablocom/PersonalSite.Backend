namespace PersonalSite.Domain.Events;

public class JobExperienceAdded : IDomainEvent
{
    public string Company { get; set; }
    public string Description { get; set; }
    public DateOnly JobPeriodStart { get; set; }
    public DateOnly? JobPeriodEnd { get; set; }
    public IEnumerable<string> TechStack { get; set; }

    public JobExperienceAdded(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, IEnumerable<string> techStack)
    {
        Company = company;
        Description = description;
        JobPeriodStart = jobPeriodStart;
        JobPeriodEnd = jobPeriodEnd;
        TechStack = techStack;
    }
}
