using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PersonalSite.Domain.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> logger;
            
        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetHealthStatus()
        {
            logger.LogInformation("Status OK...");
            return Ok("Pablo Company PersonalSite's API Rest");
        }
    }
}
