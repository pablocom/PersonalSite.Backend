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
using PersonalSite.WebApi.Converters;

namespace PersonalSite.FunctionalTests;

public class WhenCreatingAndRetrievingJobExperiences
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new DateOnlyJsonConverter() },
        PropertyNameCaseInsensitive = true
    };

    private HttpClient _client = default!;
    private readonly WebApplicationFactory<IAssemblyMarker> _applicationFactory = new();

    [SetUp]
    public void Setup()
    {
        _client = _applicationFactory.CreateClient();
    }

    [Test]
    public async Task SingleJobExperienceIsCreatedAndRetrieved()
    {
        var response = await _client.GetAsync("/jobExperiences");

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
        var createResponse = await _client.PostAsJsonAsync("/jobExperiences", createJobExperienceDto, JsonSerializerOptions);
        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var secondResponse = await _client.GetAsync("jobExperiences");
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
            await using var scope = _applicationFactory.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PersonalSiteDbContext>();
            dbContext.JobExperiences.RemoveRange(dbContext.JobExperiences);
            dbContext.SaveChanges();
        }
        _applicationFactory.Dispose();
    }
}
