using System;
using MediatR;

namespace PersonalSite.Domain.API.Commands;

public class CreateJobExperienceCommand : IRequest<Unit>
{
    public string Company { get; }
    public string Description { get; }
    public DateTime JobPeriodStart { get; }
    public DateTime? JobPeriodEnd { get; }
    public string[] TechStack { get; }

    public CreateJobExperienceCommand(string company, string description, DateTime jobPeriodStart,
        DateTime? jobPeriodEnd, string[] techStack)
    {
        Company = company;
        Description = description;
        JobPeriodStart = jobPeriodStart;
        JobPeriodEnd = jobPeriodEnd;
        TechStack = techStack;
    }
}
