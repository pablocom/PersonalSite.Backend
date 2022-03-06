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
using PersonalSite.IoC;
using PersonalSite.Persistence;
using PersonalSite.WebApi.Dtos;
using PersonalSite.WebApi.Infrastructure;

namespace PersonalSite.WebApi.IntegrationTests;

public class WhenCreatingAndRetrievingJobExperiences
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new DateOnlyJsonConverter() },
        PropertyNameCaseInsensitive = true
    };

    private HttpClient client = default!;
    private readonly WebApplicationFactory<Startup> applicationFactory = new();

    [SetUp]
    public void Setup()
    {
        client = applicationFactory.CreateClient();
    }

    [Test]
    public async Task SingleJobExperienceIsCreatedAndRetrieved()
    {
        var response = await client.GetAsync("JobExperience");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var jobExperiences = await response.Content.ReadFromJsonAsync<JobExperienceDto[]>(JsonSerializerOptions);
        Assert.IsNotNull(jobExperiences);
        Assert.IsEmpty(jobExperiences!);

        var createJobExperienceDto = new CreateJobExperienceDto(
            "Ryanair",
            "Software Developer",
            new DateOnly(2020, 1, 1),
            new DateOnly(2020, 5, 1),
            new[] { ".Net Core", "NSubstitute" });
        var createResponse = await client.PostAsJsonAsync("JobExperience", createJobExperienceDto, JsonSerializerOptions);
        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var secondResponse = await client.GetAsync("JobExperience");
        Assert.That(secondResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var json = await secondResponse.Content.ReadAsStringAsync();
        var secondJobExperiences = JsonSerializer.Deserialize<JobExperienceDto[]>(json, JsonSerializerOptions);
        Assert.That(secondJobExperiences, Has.Length.EqualTo(1));
        Assert.That(secondJobExperiences![0].Company, Is.EqualTo(createJobExperienceDto.Company));
        Assert.That(secondJobExperiences[0].Description, Is.EqualTo(createJobExperienceDto.Description));
        Assert.That(secondJobExperiences[0].JobPeriodEnd, Is.EqualTo(createJobExperienceDto.JobPeriodEnd));
        Assert.That(secondJobExperiences[0].JobPeriodStart, Is.EqualTo(createJobExperienceDto.JobPeriodStart));
        Assert.That(secondJobExperiences[0].TechStack, Is.EqualTo(createJobExperienceDto.TechStack));
    }

    [TearDown]
    public async Task Teardown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
        {
            await using var scope = applicationFactory.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PersonalSiteDbContext>();
            dbContext.JobExperiences.RemoveRange(dbContext.JobExperiences);
            dbContext.SaveChanges();
        }
        applicationFactory.Dispose();
    }
}
