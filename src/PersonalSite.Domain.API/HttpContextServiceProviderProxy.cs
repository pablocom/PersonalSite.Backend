using System;
using PersonalSite.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalSite.WebApi;

public class HttpContextServiceProviderProxy : IServiceProviderProxy
{
    private readonly IHttpContextAccessor contextAccessor;

    public HttpContextServiceProviderProxy(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    public object GetService(Type type)
    {
        return contextAccessor.HttpContext.RequestServices.GetService(type);
    }

    public TService GetService<TService>()
    {
        return contextAccessor.HttpContext.RequestServices.GetRequiredService<TService>();
    }
}