using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence;
using System.Threading.Tasks;
using PersonalSite.API.Application.Dtos;
using PersonalSite.Services;

namespace PersonalSite.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobExperienceController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> logger;
        private readonly IJobExperienceService service;
        private readonly IUnitOfWork unitOfWork;

        public JobExperienceController(ILogger<HealthCheckController> logger, IJobExperienceService service, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.service = service;
            this.unitOfWork = unitOfWork;
        }

        // GET /jobexperience
        [HttpGet]
        public async Task<IEnumerable<JobExperience>> GetAll()
        {
            return service.GetAll();
        }

        // POST /jobexperience
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobExperienceDto dto)
        {
            service.CreateJobExperience(dto.Company,
                dto.Description,
                dto.JobPeriodStart,
                dto.JobPeriodEnd,
                dto.TechStack.ToArray());
            return Ok();
        }
    }
}