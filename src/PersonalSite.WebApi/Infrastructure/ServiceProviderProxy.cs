using System;
using PersonalSite.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalSite.WebApi.Infrastructure;

public class ServiceProviderProxy : IServiceProviderProxy
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IBusEventHandlerScopeAccessor _messageBusConsumerScopeAccessor;

    public ServiceProviderProxy(
        IHttpContextAccessor contextAccessor,
        IBusEventHandlerScopeAccessor messageBusConsumerScopeAccessor)
    {
        _contextAccessor = contextAccessor;
        _messageBusConsumerScopeAccessor = messageBusConsumerScopeAccessor;
    }

    public object GetService(Type type)
    {
        if (_contextAccessor.HttpContext is not null)
            return _contextAccessor.HttpContext.RequestServices.GetService(type);

        return _messageBusConsumerScopeAccessor.CurrentScope.ServiceProvider.GetService(type);
    }

    public TService GetRequiredService<TService>()
    {
        if (_contextAccessor.HttpContext is not null)
            return _contextAccessor.HttpContext.RequestServices.GetRequiredService<TService>();

        return _messageBusConsumerScopeAccessor.CurrentScope.ServiceProvider.GetRequiredService<TService>();
    }
}