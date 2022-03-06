using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalSite.Persistence;
using Microsoft.Extensions.Logging;
using PersonalSite.Application;
using PersonalSite.IoC;
using PersonalSite.WebApi.Installers;
using PersonalSite.WebApi.Errors;
using PersonalSite.WebApi.Infrastructure;
using PersonalSite.Domain.Events;
using MassTransit;

namespace PersonalSite.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

        services.AddDbContext<PersonalSiteDbContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("PersonalSiteConnectionString")))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IJobExperienceRepository, JobExperienceRepository>()
            .AddTransient<IMigrator, PersonalSiteDbContextMigrator>()
            .AddScoped<IJobExperienceService, JobExperienceService>()
            .AddMediatR(typeof(Startup))
            .AddHttpContextAccessor().AddSingleton<IServiceProviderProxy, ServiceProviderProxy>()
            .AddDomainEventHandlers();

        AddMassTransit(services);

        RunContextMigrations(services);
    }

    private static void AddMassTransit(IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<MessageConsumer>();

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMediator(cfg =>
        {

        });
        services.AddMassTransitHostedService(true);

        services.AddHostedService<Worker>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, ILogger<Startup> logger, IServiceProvider serviceProvider)
    {
        DependencyInjectionContainer.Init(serviceProvider.GetRequiredService<IServiceProviderProxy>());

        app.ConfigureExceptionHandler(logger);

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static void RunContextMigrations(IServiceCollection services)
    {
        services.BuildServiceProvider().GetRequiredService<IMigrator>().Migrate();
    }
}
