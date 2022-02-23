using System;
using PersonalSite.IoC;
using Microsoft.AspNetCore.Http;

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
}