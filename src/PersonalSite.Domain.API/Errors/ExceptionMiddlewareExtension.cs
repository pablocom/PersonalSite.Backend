using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PersonalSite.Domain.API.Errors
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is null)
                        return;

                    context.Response.ContentType = "application/problem+json";
                    
                    logger.LogError(contextFeature.Error.ToString());
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
                    {
                        Status = (int) HttpStatusCode.InternalServerError,
                        Title = "Internal server error"
                    }));
                });
            });
        }
    }
}

    

