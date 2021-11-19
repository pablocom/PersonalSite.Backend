using System;
using PersonalSite.WebApi.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests.Builders;

public class JobExperienceBuilder
{
    private string company;
    private string description;
    private string[] techStack = Array.Empty<string>();
    private DateTime startDate;
    private DateTime endDate;

    public JobExperience Build()
    {
        return new JobExperience(company, description, startDate, endDate, techStack);
    }

    public JobExperienceBuilder WithCompany(string company)
    {
        this.company = company;
        return this;
    }

    public JobExperienceBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public JobExperienceBuilder WithTechStack(string[] techStack)
    {
        this.techStack = techStack;
        return this;
    }

    public JobExperienceBuilder WithStartDate(DateTime startDate)
    {
        this.startDate = startDate;
        return this;
    }

    public JobExperienceBuilder WithEndDate(DateTime endDate)
    {
        this.endDate = endDate;
        return this;
    }
}
