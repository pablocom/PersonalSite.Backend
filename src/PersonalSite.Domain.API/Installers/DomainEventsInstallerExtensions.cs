using Microsoft.Extensions.DependencyInjection;
using PersonalSite.Domain.Events;

namespace PersonalSite.WebApi.Installers;

public static class DomainEventsInstallerExtensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        // TODO add domain events to services using reflection
        DomainEvents.Init();
        return services;
    }
}
