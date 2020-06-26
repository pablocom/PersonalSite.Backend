using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Controllers;
using PersonalSite.Persistence;
using PersonalSite.Services;

namespace PersonalSite.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class WhenHandlingGetAllJobExperiencesRequest : ControllerTestBase<JobExperienceController>
    {
        private IJobExperienceService service;

        protected override void AdditionalSetup()
        {
            service = Substitute.For<IJobExperienceService>();
        }

        [Test]
        public void ServiceIsCalled()
        {
            Controller.GetAll();

            service.Received(1).GetJobExperiences();
        }
        
        protected override JobExperienceController GetController()
        {
            return new JobExperienceController(Substitute.For<ILogger<JobExperienceController>>(), service,
                Substitute.For<IUnitOfWork>());
        }
    }
}