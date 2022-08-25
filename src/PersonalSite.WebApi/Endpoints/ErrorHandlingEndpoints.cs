using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using PersonalSite.WebApi.Endpoints.Internal;
using System.Net;

namespace PersonalSite.WebApi.Endpoints;

public class ErrorHandlingEndpoints : IEndpoints
{
    public const string BasePath = "/error";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/error", HandleError)
            .ExcludeFromDescription();
    }

    private static IResult HandleError(HttpContext httpContext, ILogger<ErrorHandlingEndpoints> logger)
    {
        var exceptionContext = httpContext.Features.Get<IExceptionHandlerFeature>();
        logger.LogError(exceptionContext.Error.Message);

        return Results.Problem(
            statusCode: (int)HttpStatusCode.InternalServerError);        
    }
}
