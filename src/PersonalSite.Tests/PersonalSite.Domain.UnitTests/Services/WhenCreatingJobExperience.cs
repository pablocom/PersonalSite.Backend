using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests.Services;

[TestFixture]
public class WhenCreatingJobExperience : PersonalSiteDomainTestBase
{
    private IJobExperienceService service;

    protected override void AdditionalSetup()
    {
        service = new JobExperienceService(Repository);
    }

    [Test]
    public void CreatesJobExperience()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };

        service.CreateJobExperience(company, description, startDate, endDate, techStack);
        CloseContext();

        var jobExperience = Repository.GetAllJobExperiences().Single();
        AssertJobExperience(jobExperience, company, description, startDate, endDate, techStack);
    }

    [Test]
    public void RaisesDomainEvent()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };
        var handler = AssumeDomainEventHandlerWasRegistered<JobExperienceAdded>();

        service.CreateJobExperience(company, description, startDate, endDate, techStack);
        CloseContext();

        handler.Received(1).Handle(Arg.Is<JobExperienceAdded>(received => AssertEvent(received, company, description, startDate, endDate, techStack)));
    }

    [TestCase(null, null)]
    [TestCase("", null)]
    [TestCase(null, "")]
    [TestCase("", "")]
    public void RaisesArgumentExceptionIfCompanyOrDescriptionIsNullOrWhiteSpace(string company, string description)
    {
        TestDelegate action = () =>
        {
            service.CreateJobExperience(company, description, new DateOnly(), new DateOnly(), Array.Empty<string>());
        };

        var exception = Assert.Throws<DomainException>(action);
        Assert.That(exception.Message, Is.EqualTo("Job experience company and description must have value"));
    }

    private static void AssertJobExperience(JobExperience jobExperience, string company, string description, DateOnly startDate, DateOnly endDate, IEnumerable<string> techStack)
    {
        Assert.That(jobExperience.Company, Is.EqualTo(company));
        Assert.That(jobExperience.Description, Is.EqualTo(description));
        Assert.That(jobExperience.JobPeriod.Start, Is.EqualTo(startDate));
        Assert.That(jobExperience.JobPeriod.End, Is.EqualTo(endDate));
        CollectionAssert.AreEquivalent(techStack, jobExperience.TechStack);
    }

    private bool AssertEvent(JobExperienceAdded received, string company, string description, DateOnly startDate, DateOnly endDate, string[] techStack)
    {
        Assert.That(received, Is.Not.Null);
        Assert.That(received.Company, Is.EqualTo(company));
        Assert.That(received.Description, Is.EqualTo(description));
        Assert.That(received.JobPeriodStart, Is.EqualTo(startDate));
        Assert.That(received.JobPeriodEnd, Is.EqualTo(endDate));
        CollectionAssert.AreEquivalent(received.TechStack, techStack);
        return true;
    }
}
