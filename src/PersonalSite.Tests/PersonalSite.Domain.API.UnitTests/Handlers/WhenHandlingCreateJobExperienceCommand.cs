using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Application.Dtos;
using PersonalSite.WebApi.CommandHandlers;
using PersonalSite.WebApi.Commands;
using System;

namespace PersonalSite.API.UnitTests.Handlers;

[TestFixture]
public class WhenHandlingCreateJobExperienceCommand : RequestQueryHandlerTestBase<CreateJobExperienceCommandHandler, CreateJobExperienceCommand, Unit>
{
    private IJobExperienceService service;

    protected override void AdditionalSetup()
    {
        service = Substitute.For<IJobExperienceService>();
        base.AdditionalSetup();
    }

    [Test]
    public void ServiceIsCalled()
    {
        var company = "Ryanair";
        var description = "Software Developer";
        var jobPeriodStart = new DateTime(2020, 1, 1);
        var jobPeriodEnd = new DateTime(2020, 5, 1);
        var techStack = new[] { ".Net Core", "NSubstitute" };
        var createJobExperienceCommand = new CreateJobExperienceCommand(company, description, jobPeriodStart, jobPeriodEnd, techStack);

        WhenHandingRequest(createJobExperienceCommand);

        var args = new JobExperienceCreationArgs(company, description, jobPeriodStart, jobPeriodEnd, techStack);
        service.Received(1).CreateJobExperience(Arg.Is<JobExperienceCreationArgs>(x => 
            x.Company == company && 
            x.Description == description &&
            x.JobPeriodStart == jobPeriodStart &&
            x.JobPeriodEnd == jobPeriodEnd &&
            x.TechStack == techStack));
    }

    protected override CreateJobExperienceCommandHandler GetRequestHandler()
    {
        return new CreateJobExperienceCommandHandler(service, UnitOfWork);
    }
}
