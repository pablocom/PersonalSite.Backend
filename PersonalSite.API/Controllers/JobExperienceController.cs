using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalSite.Domain.API.Application.Dtos;
using PersonalSite.Persistence;
using PersonalSite.Services;

namespace PersonalSite.Domain.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobExperienceController : ControllerBase
    {
        private readonly ILogger<JobExperienceController> logger;
        private readonly IJobExperienceService service;
        private readonly IUnitOfWork unitOfWork;

        public JobExperienceController(ILogger<JobExperienceController> logger, IJobExperienceService service, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.service = service;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jobExperiences = service.GetJobExperiences();
            return Ok(jobExperiences);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateJobExperienceDto dto)
        {
            service.CreateJobExperience(dto.Company,
                dto.Description,
                dto.JobPeriodStart,
                dto.JobPeriodEnd,
                dto.TechStack);

            unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}