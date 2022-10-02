using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Domain;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests.Services;

[TestFixture]
public class WhenCreatingJobExperience : PersonalSiteDomainTestBase
{
    private IJobExperienceService _service;
    private Mock<IDomainEventPublisher> _domainEventPublisherMock;

    protected override void AdditionalSetup()
    {
        _domainEventPublisherMock = new Mock<IDomainEventPublisher>();
        _service = new JobExperienceService(Repository, _domainEventPublisherMock.Object);
    }

    [Test]
    public async Task CreatesJobExperience()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };

        await _service.CreateJobExperience(company, description, startDate, endDate, techStack);
        CloseContext();

        var jobExperience = (await Repository.GetAllJobExperiences()).Single();
        AssertJobExperience(jobExperience, company, description, startDate, endDate, techStack);
    }

    [Test]
    public async Task JobExperienceAddedEventIsPublished()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };

        await _service.CreateJobExperience(company, description, startDate, endDate, techStack);

        _domainEventPublisherMock.Verify(
            publisher => publisher.PublishAsync(It.Is<IEnumerable<IDomainEvent>>(@events => AssertJobExperienceAddedEvent(
                    @events.Single() as JobExperienceAdded, company, description, startDate, endDate, techStack))
            ),
            Times.Once
        );
    }

    [TestCase(null, null)]
    [TestCase("", null)]
    [TestCase(null, "")]
    [TestCase("", "")]
    public void RaisesArgumentExceptionIfCompanyOrDescriptionIsNullOrWhiteSpace(string company, string description)
    {
        async Task Action()
        {
            await _service.CreateJobExperience(company, description, new DateOnly(), new DateOnly(), Array.Empty<string>());
        }

        var exception = Assert.ThrowsAsync<DomainException>(Action);
        Assert.That(exception.Message, Is.EqualTo("Job experience company and description must have value"));
    }

    private static void AssertJobExperience(JobExperience jobExperience, string company, string description, DateOnly startDate, 
        DateOnly endDate, IEnumerable<string> techStack)
    {
        Assert.That(jobExperience.Company, Is.EqualTo(company));
        Assert.That(jobExperience.Description, Is.EqualTo(description));
        Assert.That(jobExperience.JobPeriod.Start, Is.EqualTo(startDate));
        Assert.That(jobExperience.JobPeriod.End, Is.EqualTo(endDate));
        CollectionAssert.AreEquivalent(techStack, jobExperience.TechStack);
    }

    private static bool AssertJobExperienceAddedEvent(JobExperienceAdded @event, string company, string description, DateOnly startDate, 
        DateOnly endDate, IEnumerable<string> techStack)
    {
        Assert.That(@event.Company, Is.EqualTo(company));
        Assert.That(@event.Description, Is.EqualTo(description));
        Assert.That(@event.JobPeriodStart, Is.EqualTo(startDate));
        Assert.That(@event.JobPeriodEnd, Is.EqualTo(endDate));
        Assert.That(@event.TechStack, Is.EquivalentTo(techStack));
        return true;
    }
}
