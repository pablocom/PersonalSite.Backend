using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PersonalSite.WebApi.Infrastructure;

namespace PersonalSite.WebApi.MessageBus;

public class MassTransitConsumerScopeInterceptorFilter<TMessage> 
    : IFilter<ConsumeContext<TMessage>> where TMessage : class
{
    private readonly IBusEventHandlerScopeAccessor _consumerScopeAccessor;

    public MassTransitConsumerScopeInterceptorFilter(IBusEventHandlerScopeAccessor consumerScopeAccessor)
    {
        _consumerScopeAccessor = consumerScopeAccessor;
    }
    
    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        _consumerScopeAccessor.CurrentScope = context.GetPayload<IServiceScope>();
        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
        context.CreateScope("ConsumeContextScope");
    }
}