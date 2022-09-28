using MediatR;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonalSite.Application;
using PersonalSite.Persistence;
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

builder.Services.AddDbContext<PersonalSiteDbContext>(options => options.UseNpgsql(builder.Configuration.GetValue<string>("PersonalSiteConnectionString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJobExperienceRepository, JobExperienceRepository>();
builder.Services.AddScoped<IMigrator, PersonalSiteDbContextMigrator>();
builder.Services.AddScoped<IJobExperienceService, JobExperienceService>();

builder.Services.AddMediatR(typeof(IAssemblyMarker));

builder.Services.AddHttpContextAccessor();

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