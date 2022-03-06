using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using System.Threading;
using System;


namespace PersonalSite.WebApi
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
                await _bus.Publish(new Message { Text = $"The time is {DateTimeOffset.Now}" });

                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}