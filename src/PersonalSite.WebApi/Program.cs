using Microsoft.EntityFrameworkCore.Migrations;
using PersonalSite.Application;
using PersonalSite.IoC;
using PersonalSite.Persistence;
using PersonalSite.WebApi.Infrastructure;
using PersonalSite.WebApi;
using MediatR;
using PersonalSite.WebApi.Installers;
using PersonalSite.WebApi.Endpoints.Internal;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using PersonalSite.WebApi.Errors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    opt.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersonalSiteDbContext>(options => options.UseNpgsql(builder.Configuration.GetValue<string>("PersonalSiteConnectionString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJobExperienceRepository, JobExperienceRepository>();
builder.Services.AddScoped<IMigrator, PersonalSiteDbContextMigrator>();
builder.Services.AddScoped<IJobExperienceService, JobExperienceService>();

builder.Services.AddMediatR(typeof(IAssemblyMarker));

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IBusEventHandlerScopeAccessor, BusEventHandlerScopeAccessor>();
builder.Services.AddSingleton<IServiceProviderProxy, ServiceProviderProxy>();
builder.Services.AddDomainEventHandlers();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseEndpoints<Program>();

using (var scope = app.Services.CreateScope())
{
    var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrator>();
    await migrationRunner.MigrateAsync();
}


DependencyInjectionContainer.Init(app.Services.GetRequiredService<IServiceProviderProxy>());

await app.RunAsync();