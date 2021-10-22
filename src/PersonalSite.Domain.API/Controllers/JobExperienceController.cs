﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Domain.API.Application.Commands;
using PersonalSite.Domain.API.Application.Dtos;
using PersonalSite.Persistence;
using System.Threading.Tasks;
using PersonalSite.Domain.API.Application.Queries;

namespace PersonalSite.Domain.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobExperienceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;

        public JobExperienceController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobExperiences = await mediator.Send(new GetJobExperiencesQuery());
            return Ok(jobExperiences);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobExperienceDto dto)
        {
            await mediator.Send(
                new CreateJobExperienceCommand(
                    dto.Company,
                    dto.Description,
                    dto.JobPeriodStart,
                    dto.JobPeriodEnd,
                    dto.TechStack)
            );

            await unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}