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
using PersonalSite.Domain.API.Errors;
using PersonalSite.Domain.Application;

namespace PersonalSite.Domain.API
{
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
            services.AddControllers();
            services.AddDbContext<PersonalSiteDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("PersonalSiteConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJobExperienceRepository, JobExperienceRepository>();
            services.AddScoped<IMigrator, PersonalSiteDbContextMigrator>();
            services.AddScoped<IJobExperienceService, JobExperienceService>();

            services.AddMediatR(typeof(Startup));

            RunContextMigrations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.ConfigureExceptionHandler(logger);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RunContextMigrations(IServiceCollection services)
        {
            services.BuildServiceProvider().GetService<IMigrator>()!.Migrate();
        }
    }
}
