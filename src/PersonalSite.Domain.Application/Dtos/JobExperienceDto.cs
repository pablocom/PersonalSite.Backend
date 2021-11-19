using System;
using System.Collections.Generic;
using PersonalSite.WebApi.Model.JobExperienceAggregate;

namespace PersonalSite.Application.Dtos;

[Serializable]
public class JobExperienceDto
{
    public string Company { get; set; }
    public string Description { get; set; }
    public DateTime JobPeriodStart { get; set; }
    public DateTime? JobPeriodEnd { get; set; }
    public IEnumerable<string> TechStack { get; set; }

    public JobExperienceDto() { }

    public JobExperienceDto(string company, string description, JobPeriod jobPeriod, IEnumerable<string> techStack)
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
