using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PersonalSite.Application.DomainEventHandlers;
using PersonalSite.Domain.Events;
using System;
using System.Linq;

namespace PersonalSite.WebApi.Installers;

public static class DomainEventsInstallerExtensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcherStore, DomainEventDispatcherStore>()
            .AddMediatR(typeof(JobExperienceAddedHandler));
        RegisterAsyncDomainEventHandlers(services);
        return services;
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
