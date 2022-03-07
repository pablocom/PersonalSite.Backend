using Microsoft.Extensions.DependencyInjection;
using PersonalSite.Domain.Events;
using System;
using System.Linq;

namespace PersonalSite.WebApi.Installers;

public static class DomainEventsInstallerExtensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcherStore, DomainEventDispatcherStore>();
        RegisterSyncDomainEventHandlers(services);
        RegisterAsyncDomainEventHandlers(services);
        return services;
    }

    private static void RegisterSyncDomainEventHandlers(IServiceCollection services)
    {
        var syncHandlerTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                    .Where(x => x.GetInterfaces().Any(y => y.IsGenericType &&
                                y.GetGenericTypeDefinition() == typeof(IHandleDomainEventsSynchronouslyInCurrentScope<>)))
                    .ToList();

        foreach (var syncHandlerType in syncHandlerTypes)
        {
            DomainEvents.RegisterSyncHandler(syncHandlerType);
            services.AddScoped(syncHandlerType);
        }
    }

    private static void RegisterAsyncDomainEventHandlers(IServiceCollection services)
    {
        var asyncHandlerTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                    .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && 
                                y.GetGenericTypeDefinition() == typeof(IHandleDomainEventsAsynchronouslyAtTheEndOfTheCurrentScope<>)))
                    .ToList();

        foreach (var asyncHandlerType in asyncHandlerTypes)
        {
            DomainEvents.RegisterAsyncHandler(asyncHandlerType);
            services.AddScoped(asyncHandlerType);
        }
    }
}
