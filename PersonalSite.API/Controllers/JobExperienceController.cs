using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalSite.Domain.API.Application.Dtos;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence;
using PersonalSite.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<JobExperience>> GetAll()
        {
            return await Task.FromResult(service.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobExperienceDto dto)
        {
            this.service.CreateJobExperience(dto.Company,
                dto.Description,
                dto.JobPeriodStart,
                dto.JobPeriodEnd,
                dto.TechStack);

            await this.unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}