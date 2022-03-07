using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalSite.WebApi.MessageBus
{
    public interface IBusEventHandlerScopeAccessor
    {
        IServiceScope? CurrentScope { get; set; }
    }

    // Implementation copied from https://github.com/dotnet/aspnetcore/blob/main/src%2FHttp%2FHttp%2Fsrc%2FHttpContextAccessor.cs
    public class BusEventHandlerScopeAccessor : IBusEventHandlerScopeAccessor
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
                    // Clear current IServiceScope trapped in the AsyncLocals, as its done.
                    holder.Context = null;
                }

                if (value != null)
                {
                    // Use an object indirection to hold the IServiceScope in the AsyncLocal,
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