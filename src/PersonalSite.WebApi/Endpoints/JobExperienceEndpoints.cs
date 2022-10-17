using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.WebApi.Commands;
using PersonalSite.WebApi.Dtos;
using PersonalSite.WebApi.Endpoints.Internal;
using PersonalSite.WebApi.Queries;
using System.Net.Mime;

namespace PersonalSite.WebApi.Endpoints;

public class JobExperienceEndpoints : IEndpoints
{
    private const string BaseRoute = "/jobExperiences";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, CreateJobExperience)
            .WithName(nameof(CreateJobExperience))
            .Accepts<CreateJobExperienceDto>(MediaTypeNames.Application.Json);

        app.MapGet(BaseRoute, GetAll)
            .WithName(nameof(GetAll))
            .Produces<IEnumerable<CreateJobExperienceDto>>(StatusCodes.Status200OK);
    }

    private static async Task<IResult> CreateJobExperience([FromBody] CreateJobExperienceDto dto, IMediator mediator)
    {
        await mediator.Send(new CreateJobExperienceCommand(dto.Company, dto.Description, dto.JobPeriodStart,
            dto.JobPeriodEnd, dto.TechStack));

        return Results.Ok();
    }

    private static async Task<IResult> GetAll(IMediator mediator)
    {
        var jobExperiences = await mediator.Send(new GetJobExperiencesQuery());
        return Results.Ok(jobExperiences);
    }
}
