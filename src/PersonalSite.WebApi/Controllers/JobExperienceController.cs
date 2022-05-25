using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PersonalSite.WebApi.Dtos;
using PersonalSite.WebApi.Queries;
using PersonalSite.WebApi.Commands;
using System;

namespace PersonalSite.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobExperienceController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobExperienceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var jobExperiences = await _mediator.Send(new GetJobExperiencesQuery());
        return Ok(jobExperiences);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobExperienceDto dto)
    {
        await _mediator.Send(
            new CreateJobExperienceCommand(
                dto.Company,
                dto.Description,
                dto.JobPeriodStart,
                dto.JobPeriodEnd,
                dto.TechStack));
        return Ok();
    }
}
