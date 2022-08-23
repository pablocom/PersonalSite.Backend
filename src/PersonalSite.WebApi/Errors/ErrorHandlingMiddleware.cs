using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace PersonalSite.WebApi.Errors;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        _logger.LogError(exception.ToString());
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Internal server error"
        }));
    }
}
