using NUnit.Framework;
using PersonalSite.API.Controllers;

namespace PersonalSite.Tests.UnitTests
{
    public class WhenJobExperiencesControllerHandlesCreateRequest : ControllerTestBase<JobExperienceController>
    {
        [Test]
        public void ReturnsOk()
        {
            Assert.That(true, Is.True);
        }

        public override JobExperienceController GetController()
        {
            return null;
        }
    }
}