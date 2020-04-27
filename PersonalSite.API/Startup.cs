using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalSite.Persistence;

namespace PersonalSite.API
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
                options.UseMySql(Configuration.GetConnectionString("PersonalSiteDatabase")));

            services.AddScoped<DbContext, PersonalSiteDbContext>();
            services.AddScoped<IMigrator, PersonalSiteDbContextMigrator>();

            RunContextMigrations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RunContextMigrations(IServiceCollection services)
        {
            services.BuildServiceProvider().GetService<IMigrator>().Migrate();
        }
    }
}
