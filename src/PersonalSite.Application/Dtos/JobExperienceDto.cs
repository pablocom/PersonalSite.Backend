using System;
using System.Collections.Generic;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Application.Dtos;

[Serializable]
public class JobExperienceDto
{
    public string Company { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly JobPeriodStart { get; set; }
    public DateOnly? JobPeriodEnd { get; set; }
    public IEnumerable<string> TechStack { get; set; } = default!;

    public JobExperienceDto()
    {
        // Parameterless ctor for deserialization
    }
    
    private JobExperienceDto(string company, string description, JobPeriod jobPeriod, IEnumerable<string> techStack)
    {
        Company = company;
        Description = description;
        JobPeriodStart = jobPeriod.Start;
        JobPeriodEnd = jobPeriod.End;
        TechStack = techStack;
    }

    public static JobExperienceDto From(JobExperience jobExperience)
    {
        return new JobExperienceDto(jobExperience.Company, jobExperience.Description, jobExperience.JobPeriod, jobExperience.TechStack);
    }
}
