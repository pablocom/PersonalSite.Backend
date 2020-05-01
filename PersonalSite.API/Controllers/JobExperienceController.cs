using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence;

namespace PersonalSite.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobExperienceController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> logger;
        private readonly IPersonalSiteRepository repository;

        public JobExperienceController(ILogger<HealthCheckController> logger, IPersonalSiteRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        // POST /jobexperience
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var jobExperience =
                new JobExperience("Ryanair", "Software developer", null, new[] {".Net Core", "EF Core"});

            repository.Add(jobExperience);

            await repository.UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}