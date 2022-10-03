using Microsoft.EntityFrameworkCore;
using PersonalSite.IntegrationEventsPublishWorker.EventPublisher;
using PersonalSite.Persistence;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((builderContext, services) =>
{
    services.AddHostedService<IntegrationEventsPublishHeartbeat>();
    services.AddDbContext<PersonalSiteDbContext>(options =>
    {
        options.UseNpgsql(builderContext.Configuration.GetValue<string>("PersonalSiteConnectionString"), b =>
        {
            b.MigrationsAssembly(typeof(PersonalSite.Persistence.Npgsql.IAssemblyMarker).Assembly.FullName);
        });
    });
    services.AddScoped<IntegrationEventsPublisher>();
    services.AddScoped<IMessageBusPublisher, DummyMessageBusPublisher>();
});

var host = builder.Build();

await host.RunAsync();
