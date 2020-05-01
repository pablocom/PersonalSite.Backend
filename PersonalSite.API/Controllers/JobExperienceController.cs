using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence;
using System.Threading.Tasks;

namespace PersonalSite.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobExperienceController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> logger;
        private readonly IPersonalSiteRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public JobExperienceController(ILogger<HealthCheckController> logger, IPersonalSiteRepository repository, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        // POST /jobexperience
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var jobExperience = new JobExperience("Loterias", "Backend developer", null, new[] {"Node.js", "Angular"});
            repository.Add(jobExperience);

            return Ok();
        }
    }
}