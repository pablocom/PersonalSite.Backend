using System;
using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Application.Commands;
using PersonalSite.Domain.API.Application.Dtos;
using PersonalSite.Domain.API.Controllers;

namespace PersonalSite.API.UnitTests.Controllers
{
    [TestFixture]
    public class WhenHandlingCreateRequest : ControllerTestBase<JobExperienceController>
    {
        private IMediator _mediator;

        protected override void AdditionalSetup()
        {
            _mediator = Substitute.For<IMediator>();
        }

        [Test]
        public async Task ServiceIsCalled()
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

            await Controller.Create(createJobExperienceDto);

            await _mediator.Received(1).Send(Arg.Is<CreateJobExperienceCommand>(x =>
                    AssertCommand(x, company, description, jobPeriodStart, jobPeriodEnd, techStack)));
        }

        private bool AssertCommand(CreateJobExperienceCommand createJobExperienceCommand, string company,
            string description, DateTime start, DateTime end, string[] teckStack)
        {
            Assert.That(createJobExperienceCommand.Company, Is.EqualTo(company));
            Assert.That(createJobExperienceCommand.Description, Is.EqualTo(description));
            Assert.That(createJobExperienceCommand.JobPeriodStart, Is.EqualTo(start));
            Assert.That(createJobExperienceCommand.JobPeriodEnd, Is.EqualTo(end));
            CollectionAssert.AreEquivalent(teckStack, createJobExperienceCommand.TechStack);
            return true;
        }

        protected override JobExperienceController GetController()
        {
            return new JobExperienceController(Logger, UnitOfWork, _mediator);
        }
    }
}