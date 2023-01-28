using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using PersonalSite.Domain;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using Xunit;

namespace PersonalSite.UnitTests.Services;

public class WhenCreatingJobExperience : PersonalSiteDomainTestBase
{
    private readonly IJobExperienceService _service;
    private readonly IDomainEventPublisher _domainEventPublisher;

    public WhenCreatingJobExperience()
    {
        _domainEventPublisher = Substitute.For<IDomainEventPublisher>();
        _service = new JobExperienceService(Repository, _domainEventPublisher);
    }

    [Fact]
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

    [Fact]
    public async Task JobExperienceAddedEventIsPublished()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };

        await _service.CreateJobExperience(company, description, startDate, endDate, techStack);

        await _domainEventPublisher.Received(1).Publish(
            Arg.Is<IEnumerable<IDomainEvent>>(@events => AssertJobExperienceAddedEvent(@events.Single() as JobExperienceAdded, company, description, startDate, endDate, techStack)
        ));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData(null, "")]
    [InlineData("", "")]
    public async Task RaisesArgumentExceptionIfCompanyOrDescriptionIsNullOrWhiteSpace(string company, string description)
    {
        async Task Action()
        {
            await _service.CreateJobExperience(company, description, new DateOnly(), new DateOnly(), Array.Empty<string>());
        }

        var exception = await Assert.ThrowsAsync<DomainException>(Action);
        Assert.Equal("Job experience company and description must have value", exception.Message);
    }

    private static void AssertJobExperience(JobExperience jobExperience, string company, string description, DateOnly startDate, 
        DateOnly endDate, IEnumerable<string> techStack)
    {
        Assert.Equal(jobExperience.Company, company);
        Assert.Equal(jobExperience.Description, description);
        Assert.Equal(jobExperience.JobPeriod.Start, startDate);
        Assert.Equal(jobExperience.JobPeriod.End, endDate);
        Assert.Equal(techStack, jobExperience.TechStack);
    }

    private static bool AssertJobExperienceAddedEvent(JobExperienceAdded @event, string company, string description, DateOnly startDate, 
        DateOnly endDate, IEnumerable<string> techStack)
    {
        Assert.Equal(@event.Company, company);
        Assert.Equal(@event.Description, description);
        Assert.Equal(@event.JobPeriodStart, startDate);
        Assert.Equal(@event.JobPeriodEnd, endDate);
        Assert.Equal(@event.TechStack, techStack);
        return true;
    }
}
