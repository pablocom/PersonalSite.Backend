using System;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalSite.WebApi.MessageBus;

public class ConsumerScopeAccessorMiddleware<TMessage> : IFilter<ConsumeContext<TMessage>> where TMessage : class
{
    private readonly IServiceProvider serviceProvider;
    private readonly IMessageBusConsumerScopeAccessor consumerScopeAccessor;

    public ConsumerScopeAccessorMiddleware(IServiceProvider serviceProvider, IMessageBusConsumerScopeAccessor consumerScopeAccessor)
    {
        this.serviceProvider = serviceProvider;
        this.consumerScopeAccessor = consumerScopeAccessor;
    }
    
    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        using var scope = serviceProvider.CreateScope();
        consumerScopeAccessor.CurrentScope = scope;
        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
        context.CreateScope("ConsumeContextScope");
    }
}