using System;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Domain.Events;
using PersonalSite.WebApi.CommandHandlers;
using PersonalSite.WebApi.Commands;

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
        var jobPeriodStart = new DateOnly(2020, 1, 1);
        var jobPeriodEnd = new DateOnly(2020, 5, 1);
        var techStack = new[] { ".Net Core", "NSubstitute" };

        var createJobExperienceCommand = new CreateJobExperienceCommand(company, description, jobPeriodStart, jobPeriodEnd, techStack);
        WhenHandingRequest(createJobExperienceCommand);

        service.Received(1).CreateJobExperience(company, description, jobPeriodStart, jobPeriodEnd, techStack);
    }

    protected override CreateJobExperienceCommandHandler GetRequestHandler()
    {
        return new CreateJobExperienceCommandHandler(service, UnitOfWork, Substitute.For<IDomainEventDispatcherStore>());
    }
}
