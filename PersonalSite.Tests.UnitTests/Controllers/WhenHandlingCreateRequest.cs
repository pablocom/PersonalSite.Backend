using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Application.Dtos;
using PersonalSite.Domain.API.Controllers;
using PersonalSite.Persistence;
using PersonalSite.Services;
using System;

namespace PersonalSite.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class WhenHandlingCreateRequest : ControllerTestBase<JobExperienceController>
    {
        private IJobExperienceService service;

        protected override void AdditionalSetup()
        {
            service = Substitute.For<IJobExperienceService>();
        }

        [Test]
        public void ServiceIsCalled()
        {
            var company = "Ryanair";
            var description = "Software Developer";
            var jobPeriodStart = new DateTime(2020, 1, 1);
            var jobPeriodEnd = new DateTime(2020, 5, 1);
            var techStack = new[] {".Net Core", "NSubstitute"};
            var createJobExperienceDto = new CreateJobExperienceDto
            {
                Company = company,
                Description = description,
                JobPeriodStart = jobPeriodStart,
                JobPeriodEnd = jobPeriodEnd,
                TechStack = techStack
            };

            Controller.Create(createJobExperienceDto);

            service.Received(1).CreateJobExperience(Arg.Is(company), Arg.Is(description), Arg.Is(jobPeriodStart),
                Arg.Is(jobPeriodEnd), Arg.Is(techStack));
        }

        protected override JobExperienceController GetController()
        {
            return new JobExperienceController(Substitute.For<ILogger<JobExperienceController>>(), service,
                Substitute.For<IUnitOfWork>(), Substitute.For<IMediator>());
        }
    }
}