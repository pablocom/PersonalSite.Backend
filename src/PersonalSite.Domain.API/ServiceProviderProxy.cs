using System;
using PersonalSite.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PersonalSite.WebApi.MessageBus;

namespace PersonalSite.WebApi;

public class ServiceProviderProxy : IServiceProviderProxy
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly IMessageBusConsumerScopeAccessor messageBusConsumerScopeAccessor;
    private readonly IServiceProvider serviceProvider;
    
    public ServiceProviderProxy(
        IHttpContextAccessor contextAccessor,
        IMessageBusConsumerScopeAccessor messageBusConsumerScopeAccessor,
        IServiceProvider serviceProvider)
    {
        this.contextAccessor = contextAccessor;
        this.messageBusConsumerScopeAccessor = messageBusConsumerScopeAccessor;
        this.serviceProvider = serviceProvider;
    }

    public object GetService(Type type)
    {
        if (contextAccessor.HttpContext is not null)
            return contextAccessor.HttpContext.RequestServices.GetService(type);

        return messageBusConsumerScopeAccessor.CurrentScope.ServiceProvider.GetService(type);
    }

    public TService GetService<TService>()
    {
        if (contextAccessor.HttpContext is not null)
            return contextAccessor.HttpContext.RequestServices.GetRequiredService<TService>();

        return messageBusConsumerScopeAccessor.CurrentScope.ServiceProvider.GetRequiredService<TService>();
    }

    public IServiceScope BeginScope()
    {
        return serviceProvider.CreateScope();
    }
}