using System.Threading;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalSite.WebApi.MessageBus
{
    public interface IMessageBusConsumerScopeAccessor
    {
        IServiceScope? CurrentScope { get; set; }
    }
    
    public class MessageBusConsumerScopeAccessor : IMessageBusConsumerScopeAccessor
    {
        private static readonly AsyncLocal<MessageBusScopeHolder> _httpContextCurrent = new();

        public IServiceScope? CurrentScope
        {
            get { return _httpContextCurrent.Value?.Context; }
            set
            {
                var holder = _httpContextCurrent.Value;
                if (holder != null)
                {
                    // Clear current HttpContext trapped in the AsyncLocals, as its done.
                    holder.Context = null;
                }

                if (value != null)
                {
                    // Use an object indirection to hold the HttpContext in the AsyncLocal,
                    // so it can be cleared in all ExecutionContexts when its cleared.
                    _httpContextCurrent.Value = new MessageBusScopeHolder {Context = value};
                }
            }
        }

        private class MessageBusScopeHolder
        {
            public IServiceScope? Context;
        }
    }

}