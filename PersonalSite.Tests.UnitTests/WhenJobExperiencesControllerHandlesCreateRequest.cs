using System;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.API.Application.Dtos;
using PersonalSite.API.Controllers;
using PersonalSite.Persistence;
using PersonalSite.Services;

namespace PersonalSite.Tests.UnitTests
{
    public class WhenJobExperiencesControllerHandlesCreateRequest : ControllerTestBase<JobExperienceController>
    {
        private IJobExperienceService service;
        private JobExperienceController controller;

        [SetUp]
        public void SetUp()
        {
            service = Substitute.For<IJobExperienceService>();
            controller = GetController();
        }

        [Test]
        public void ServiceIsCalled()
        {
            var company = "Ryanair";
            var description = "Software Developer";
            var jobPeriodStart = new DateTime(2020, 1, 1);
            var jobPeriodEnd = new DateTime(2020, 5, 1);
            var techStack = new[] {".Net Core", "NSubstitute"};

            _ = controller.Create(new CreateJobExperienceDto
            {
                Company = company,
                Description = description,
                JobPeriodStart = jobPeriodStart,
                JobPeriodEnd = jobPeriodEnd,
                TechStack = techStack
            });

            service.Received(1).CreateJobExperience(Arg.Is(company), Arg.Is(description), Arg.Is(jobPeriodStart), Arg.Is(jobPeriodEnd), Arg.Is(techStack));
        }

        public override JobExperienceController GetController()
        {
            return new JobExperienceController(Substitute.For<ILogger<JobExperienceController>>(), service, Substitute.For<IUnitOfWork>());
        }
    }
}