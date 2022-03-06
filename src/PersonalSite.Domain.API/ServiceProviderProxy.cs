using System;
using PersonalSite.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalSite.WebApi;

public class ServiceProviderProxy : IServiceProviderProxy
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly IServiceProvider serviceProvider; // TODO: this should be the masstransit consumer context accessor

    public ServiceProviderProxy(IHttpContextAccessor contextAccessor, IServiceProvider serviceProvider)
    {
        this.contextAccessor = contextAccessor;
        this.serviceProvider = serviceProvider;
    }

    public object GetService(Type type)
    {
        if (contextAccessor.HttpContext is not null)
            return contextAccessor.HttpContext.RequestServices.GetService(type);

        return serviceProvider.GetService(type);
    }

    public TService GetService<TService>()
    {
        if (contextAccessor.HttpContext is not null)
            return contextAccessor.HttpContext.RequestServices.GetRequiredService<TService>();

        return serviceProvider.GetRequiredService<TService>();
    }
}