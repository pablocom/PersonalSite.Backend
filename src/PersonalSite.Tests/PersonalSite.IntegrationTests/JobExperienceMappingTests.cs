using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PersonalSite.IntegrationTests;

public class JobExperienceMappingTests : PersonalSiteIntegrationTestBase
{
    [Fact]
    public async Task IsPersisted()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };

        await Repository.Add(new JobExperience(company, description, startDate, endDate, techStack));
        await SaveChangesAndClearTracking();

        var jobExperiences = await Repository.GetAllJobExperiences();
        Assert.Single(jobExperiences);
        AssertJobExperience(jobExperiences.First(), company, description, startDate, endDate, techStack);
    }

    private static void AssertJobExperience(JobExperience jobExperience, string company, string description, DateOnly startDate, DateOnly endDate, IEnumerable<string> techStack)
    {
        Assert.Equal(jobExperience.Company, company);
        Assert.Equal(jobExperience.Description, description);
        Assert.Equal(jobExperience.JobPeriod.Start, startDate);
        Assert.Equal(jobExperience.JobPeriod.End, endDate);
        Assert.Equal(techStack, jobExperience.TechStack);
    }
}
