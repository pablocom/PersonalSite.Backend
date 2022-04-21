using NUnit.Framework;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalSite.IntegrationTests;

[TestFixture]
public class JobExperienceMappingTests : PersonalSiteIntegrationTestBase
{
    [Test]
    public void IsPersisted()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };

        Repository.Add(new JobExperience(company, description, startDate, endDate, techStack));
        CloseContext();

        var jobExperiences = Repository.GetAllJobExperiences().ToArray();
        Assert.That(jobExperiences, Has.Length.EqualTo(1));
        AssertJobExperience(jobExperiences[0], company, description, startDate, endDate, techStack);
    }

    private static void AssertJobExperience(JobExperience jobExperience, string company, string description, DateOnly startDate, DateOnly endDate, IEnumerable<string> techStack)
    {
        Assert.That(jobExperience.Company, Is.EqualTo(company));
        Assert.That(jobExperience.Description, Is.EqualTo(description));
        Assert.That(jobExperience.JobPeriod.Start, Is.EqualTo(startDate));
        Assert.That(jobExperience.JobPeriod.End, Is.EqualTo(endDate));
        CollectionAssert.AreEquivalent(techStack, jobExperience.TechStack);
    }
}
