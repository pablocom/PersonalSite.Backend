using MediatR;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonalSite.Application;
using PersonalSite.Domain;
using PersonalSite.Persistence;
using PersonalSite.Persistence.Npgsql;
using PersonalSite.WebApi;
using PersonalSite.WebApi.Converters;
using PersonalSite.WebApi.Endpoints.Internal;
using PersonalSite.WebApi.Errors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    opt.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersonalSiteDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetValue<string>("PersonalSiteConnectionString"), b =>
    {
        b.MigrationsAssembly(typeof(PersonalSite.Persistence.Npgsql.IAssemblyMarker).Assembly.FullName);
    });
});

builder.Services.AddScoped<IDomainEventPublisher, MediatRDomainEventPublisher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJobExperienceRepository, JobExperienceRepository>();
builder.Services.AddScoped<IMigrator, PersonalSiteDbMigrator>();
builder.Services.AddScoped<IJobExperienceService, JobExperienceService>();
builder.Services.AddSingleton<IClock, Clock>();

builder.Services.AddMediatR(typeof(PersonalSite.WebApi.IAssemblyMarker));
builder.Services.AddMediatR(typeof(PersonalSite.Persistence.IAssemblyMarker));

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

await app.RunAsync();