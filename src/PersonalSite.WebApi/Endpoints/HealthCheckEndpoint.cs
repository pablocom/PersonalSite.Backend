using PersonalSite.WebApi.Endpoints.Internal;

namespace PersonalSite.WebApi.Endpoints;

public class HealthCheckEndpoint : IEndpoints
{
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/healthCheck", HealthCheck)
            .WithName(nameof(HealthCheck));
    }

    public static IResult HealthCheck(ILogger<HealthCheckEndpoint> logger)
    {
        logger.LogInformation("Status OK...");
        return Results.Ok("Ha! This is a response from a .NET Minimal API");
    }
}
