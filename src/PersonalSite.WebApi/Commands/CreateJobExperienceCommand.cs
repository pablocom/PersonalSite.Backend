using MediatR;
using System;

namespace PersonalSite.WebApi.Commands;

public class CreateJobExperienceCommand : IRequest<Unit>
{
    public string Company { get; }
    public string Description { get; }
    public DateOnly JobPeriodStart { get; }
    public DateOnly? JobPeriodEnd { get; }
    public string[] TechStack { get; }

    public CreateJobExperienceCommand(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, string[] techStack)
    {
        Company = company;
        Description = description;
        JobPeriodStart = jobPeriodStart;
        JobPeriodEnd = jobPeriodEnd;
        TechStack = techStack;
    }
}
