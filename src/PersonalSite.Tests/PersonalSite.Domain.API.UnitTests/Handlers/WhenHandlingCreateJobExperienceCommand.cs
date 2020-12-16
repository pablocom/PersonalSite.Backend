using System;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Application.CommandHandlers;
using PersonalSite.Domain.API.Application.Commands;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Domain.Services;

namespace PersonalSite.API.UnitTests.Handlers
{
    [TestFixture]
    public class WhenHandlingCreateJobExperienceCommand : RequestQueryHandlerTestBase<CreateJobExperienceCommandHandler, CreateJobExperienceCommand, bool>
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
            var techStack = new[] {".Net Core", "NSubstitute"};
            
            var createJobExperienceCommand = new CreateJobExperienceCommand(company, description, jobPeriodStart, jobPeriodEnd, techStack);
            WhenHandingRequest(createJobExperienceCommand);

            service.Received(1).CreateJobExperience(company, description, jobPeriodStart, jobPeriodEnd, techStack);
        }

        protected override CreateJobExperienceCommandHandler GetRequestHandler()
        {
            return new CreateJobExperienceCommandHandler(service);
        }
    }
}