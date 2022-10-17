using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Application.Dtos;
using PersonalSite.Domain;
using PersonalSite.UnitTests.Builders;

namespace PersonalSite.UnitTests.Services;

public class WhenGettingJobExperiences : PersonalSiteDomainTestBase
{
    private IJobExperienceService _service;

    protected override void AdditionalSetup()
    {
        _service = new JobExperienceService(Repository, Mock.Of<IDomainEventPublisher>());
    }

    [Test]
    public async Task MultipleJobExperiencesAreReturned()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };

        var otherCompany = "1millionBot";
        var otherDescription = "Web Developer";
        var otherStartDate = new DateOnly(2018, 02, 09);
        var otherEndDate = new DateOnly(2018, 09, 01);
        var otherTechStack = new[] { "Node.js", "MongoDB" };

        await AssumeDataInRepository(new[]
        {
            new JobExperienceBuilder()
                .WithCompany(company)
                .WithDescription(description)
                .WithStartDate(startDate)
                .WithEndDate(endDate)
                .WithTechStack(techStack)
                .Build(),
            new JobExperienceBuilder()
                .WithCompany(otherCompany)
                .WithDescription(otherDescription)
                .WithStartDate(otherStartDate)
                .WithEndDate(otherEndDate)
                .WithTechStack(otherTechStack)
                .Build()
        });

        var jobExperiences = (await _service.GetJobExperiences()).ToArray();

        Assert.That(jobExperiences.Length, Is.EqualTo(2));
        AssertJobExperience(jobExperiences[0], company, description, startDate, endDate, techStack);
        AssertJobExperience(jobExperiences[1], otherCompany, otherDescription, otherStartDate, otherEndDate, otherTechStack);
    }

    private static void AssertJobExperience(JobExperienceDto jobExperience, string company, string description, DateOnly startDate, DateOnly endDate, string[] techStack)
    {
        Assert.That(jobExperience.Company, Is.EqualTo(company));
        Assert.That(jobExperience.Description, Is.EqualTo(description));
        Assert.That(jobExperience.JobPeriodStart, Is.EqualTo(startDate));
        Assert.That(jobExperience.JobPeriodEnd, Is.EqualTo(endDate));
        CollectionAssert.AreEquivalent(techStack, jobExperience.TechStack);
    }
}
