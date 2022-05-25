using System;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests.Builders;

public class JobExperienceBuilder
{
    private string _company;
    private string _description;
    private string[] _techStack = Array.Empty<string>();
    private DateOnly _startDate;
    private DateOnly _endDate;

    public JobExperience Build()
    {
        return new JobExperience(_company, _description, _startDate, _endDate, _techStack);
    }

    public JobExperienceBuilder WithCompany(string company)
    {
        _company = company;
        return this;
    }

    public JobExperienceBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public JobExperienceBuilder WithTechStack(string[] techStack)
    {
        _techStack = techStack;
        return this;
    }

    public JobExperienceBuilder WithStartDate(DateOnly startDate)
    {
        _startDate = startDate;
        return this;
    }

    public JobExperienceBuilder WithEndDate(DateOnly endDate)
    {
        _endDate = endDate;
        return this;
    }
}
