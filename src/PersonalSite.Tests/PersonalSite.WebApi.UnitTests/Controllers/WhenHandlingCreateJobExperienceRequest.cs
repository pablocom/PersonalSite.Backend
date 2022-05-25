using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.WebApi.Commands;
using PersonalSite.WebApi.Controllers;
using PersonalSite.WebApi.Dtos;
using System;
using System.Threading.Tasks;

namespace PersonalSite.API.UnitTests.Controllers;

[TestFixture]
public class WhenHandlingCreateJobExperienceRequest : ControllerTestBase<JobExperienceController>
{
    private IMediator _mediator;

    protected override void AdditionalSetup()
    {
        _mediator = Substitute.For<IMediator>();
    }

    protected override JobExperienceController GetController() => new(_mediator);

    [Test]
    public async Task CreateJobExperienceCommandIsSent()
    {
        var company = "Ryanair";
        var description = "Software Developer";
        var jobPeriodStart = new DateOnly(2020, 1, 1);
        var jobPeriodEnd = new DateOnly(2020, 5, 1);
        var techStack = new[] { ".Net Core", "NSubstitute" };
        var createJobExperienceDto = new CreateJobExperienceDto
        {
            Company = company,
            Description = description,
            JobPeriodStart = jobPeriodStart,
            JobPeriodEnd = jobPeriodEnd,
            TechStack = techStack
        };

        await Controller.Create(createJobExperienceDto);

        await _mediator.Received(1).Send(Arg.Is<CreateJobExperienceCommand>(x =>
                AssertCommand(x, company, description, jobPeriodStart, jobPeriodEnd, techStack)));
    }

    private static bool AssertCommand(CreateJobExperienceCommand createJobExperienceCommand, string company, string description, DateOnly start, DateOnly end, string[] teckStack)
    {
        Assert.That(createJobExperienceCommand.Company, Is.EqualTo(company));
        Assert.That(createJobExperienceCommand.Description, Is.EqualTo(description));
        Assert.That(createJobExperienceCommand.JobPeriodStart, Is.EqualTo(start));
        Assert.That(createJobExperienceCommand.JobPeriodEnd, Is.EqualTo(end));
        CollectionAssert.AreEquivalent(teckStack, createJobExperienceCommand.TechStack);
        return true;
    }
}
