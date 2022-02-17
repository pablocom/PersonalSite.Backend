using Microsoft.Extensions.DependencyInjection;
using PersonalSite.Application.DomainEventHandlers;
using PersonalSite.Domain.Events;

namespace PersonalSite.WebApi.Installers;

public static class DomainEventsInstallerExtensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        // TODO: add domain events to services using reflection
        DomainEvents.RegisterSyncHandler(typeof(JobExperienceAddedHandler));

        services.AddScoped<JobExperienceAddedHandler>();
        return services;
    }
}
