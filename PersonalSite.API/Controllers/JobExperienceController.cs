using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalSite.Domain.API.Application.Dtos;
using PersonalSite.Persistence;
using PersonalSite.Server.Queries;
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
        private readonly IMediator mediator;

        public JobExperienceController(ILogger<JobExperienceController> logger, IJobExperienceService service,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            this.logger = logger;
            this.service = service;
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobExperiences = await mediator.Send(new GetJobExperiencesQuery());
            logger.LogInformation($"Handled GetJobExperiencesQuery; {jobExperiences.Count()} matching job experiences.");
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