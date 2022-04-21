using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace PersonalSite.WebApi.MessageBus
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.WhenAll(
                    _bus.Publish(new Message { Text = $"1 - The time is {DateTimeOffset.Now}" }), 
                    _bus.Publish(new Message { Text = $"2 - The time is {DateTimeOffset.Now}" })
                );

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}