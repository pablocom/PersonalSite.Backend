using Microsoft.Extensions.DependencyInjection;
using PersonalSite.Domain.Events;

namespace PersonalSite.WebApi.Installers;

public static class DomainEventsInstallerExtensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        DomainEvents.Init();
        return services;
    }
}
