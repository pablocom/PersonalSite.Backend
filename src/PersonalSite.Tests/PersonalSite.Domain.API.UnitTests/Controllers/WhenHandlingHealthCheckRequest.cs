using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Controllers;

namespace PersonalSite.API.UnitTests.Controllers
{
    [TestFixture]
    public class WhenHandlingHealthCheckRequest : ControllerTestBase<HealthCheckController>
    {
        [Test]
        public void StatusShouldBeOk()
        {
            var expectedMessage = "Pablo Company PersonalSite's API Rest";

            var result = Controller.GetHealthStatus() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(expectedMessage));
        }

        protected override HealthCheckController GetController() =>
            new HealthCheckController(Substitute.For<ILogger<HealthCheckController>>());
    }
}