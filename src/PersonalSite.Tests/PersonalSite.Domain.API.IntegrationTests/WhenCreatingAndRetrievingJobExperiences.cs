using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PersonalSite.Application.Dtos;
using PersonalSite.Persistence;
using PersonalSite.WebApi;
using PersonalSite.WebApi.Dtos;
using PersonalSite.WebApi.Infrastructure;

namespace PersonalSite.Domain.API.FunctionalTests;

public class WhenCreatingAndRetrievingJobExperiences
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        Converters = { new DateOnlyJsonConverter() }
    };

    private HttpClient client;
    private readonly WebApplicationFactory<Startup> applicationFactory = new();
    private IServiceScope serviceScope;

    [SetUp]
    public void Setup()
    {
        client = applicationFactory.CreateClient();
        serviceScope = applicationFactory.Services.CreateScope();
    }

    [Test]
    public async Task SingleJobExperienceIsCreatedAndRetrieved()
    {
        var response = await client.GetAsync("JobExperience");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var jobExperiences = await response.Content.ReadFromJsonAsync<JobExperienceDto[]>(jsonSerializerOptions);
        Assert.IsNotNull(jobExperiences);
        Assert.IsEmpty(jobExperiences!);

        var createJobExperienceDto = new CreateJobExperienceDto(
            "Ryanair",
            "Software Developer",
            new DateOnly(2020, 1, 1),
            new DateOnly(2020, 5, 1),
            new[] { ".Net Core", "NSubstitute" });
        var createResponse = await client.PostAsJsonAsync("JobExperience", createJobExperienceDto, jsonSerializerOptions);
        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var secondResponse = await client.GetAsync("JobExperience");
        Assert.That(secondResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var json = await secondResponse.Content.ReadAsStringAsync();
        var secondJobExperiences = JsonSerializer.Deserialize<JobExperienceDto[]>(json, jsonSerializerOptions);
        Assert.That(secondJobExperiences, Has.Length.EqualTo(1));
        Assert.That(secondJobExperiences![0].Company, Is.EqualTo(createJobExperienceDto.Company));
        Assert.That(secondJobExperiences[0].Description, Is.EqualTo(createJobExperienceDto.Description));
        Assert.That(secondJobExperiences[0].JobPeriodEnd, Is.EqualTo(createJobExperienceDto.JobPeriodEnd));
        Assert.That(secondJobExperiences[0].JobPeriodStart, Is.EqualTo(createJobExperienceDto.JobPeriodStart));
        Assert.That(secondJobExperiences[0].TechStack, Is.EqualTo(createJobExperienceDto.TechStack));
    }

    [TearDown]
    public void Teardown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<PersonalSiteDbContext>();
            dbContext.JobExperiences.RemoveRange(dbContext.JobExperiences);
            dbContext.SaveChanges();
        }

        serviceScope.Dispose();
        applicationFactory.Dispose();
    }
}
