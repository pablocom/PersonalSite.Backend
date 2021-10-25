using System;
using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Commands;
using PersonalSite.Domain.API.Controllers;
using PersonalSite.Domain.API.Dtos;

namespace PersonalSite.API.UnitTests.Controllers
{
    [TestFixture]
    public class WhenHandlingCreateRequest : ControllerTestBase<JobExperienceController>
    {
        private IMediator mediator;

        protected override void AdditionalSetup()
        {
            mediator = Substitute.For<IMediator>();
        }

        protected override JobExperienceController GetController() => new(mediator);

        [Test]
        public async Task CreateJobExperienceCommandIsSent()
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

            await mediator.Received(1).Send(Arg.Is<CreateJobExperienceCommand>(x =>
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
    }
}