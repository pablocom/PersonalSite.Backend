using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PersonalSite.Application.Dtos;
using PersonalSite.Persistence;
using PersonalSite.WebApi.Dtos;
using PersonalSite.WebApi.Converters;
using Xunit;

namespace PersonalSite.FunctionalTests;

public class WhenCreatingAndRetrievingJobExperiences : IDisposable
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new DateOnlyJsonConverter() },
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<PersonalSite.WebApi.IAssemblyMarker> _applicationFactory = new();

    public WhenCreatingAndRetrievingJobExperiences()
    {
        _client = _applicationFactory.CreateClient();
    }

    [Fact]
    public async Task SingleJobExperienceIsCreatedAndRetrieved()
    {
        var response = await _client.GetAsync("/jobExperiences");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var jobExperiences = await response.Content.ReadFromJsonAsync<JobExperienceDto[]>(JsonSerializerOptions);
        Assert.NotNull(jobExperiences);
        Assert.Empty(jobExperiences!);

        var createJobExperienceDto = new CreateJobExperienceDto(
            "Ryanair",
            "Software Developer",
            new DateOnly(2020, 1, 1),
            new DateOnly(2020, 5, 1),
            new[] { ".Net Core", "NSubstitute" });
        var createResponse = await _client.PostAsJsonAsync("/jobExperiences", createJobExperienceDto, JsonSerializerOptions);
        Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

        var secondResponse = await _client.GetAsync("/jobExperiences");
        Assert.Equal(HttpStatusCode.OK, secondResponse.StatusCode);
        var createdJobExperiences = await secondResponse.Content.ReadFromJsonAsync<JobExperienceDto[]>();
        Assert.NotNull(createdJobExperiences);
        Assert.Equal(createdJobExperiences[0].Company, createJobExperienceDto.Company);
        Assert.Equal(createdJobExperiences[0].Description, createJobExperienceDto.Description);
        Assert.Equal(createdJobExperiences[0].JobPeriodEnd, createJobExperienceDto.JobPeriodEnd);
        Assert.Equal(createdJobExperiences[0].JobPeriodStart, createJobExperienceDto.JobPeriodStart);
        Assert.Equal(createdJobExperiences[0].TechStack, createJobExperienceDto.TechStack);
    }

    public void Dispose()
    {
        using var scope = _applicationFactory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PersonalSiteDbContext>();
        dbContext.JobExperiences.RemoveRange(dbContext.JobExperiences);
        dbContext.IntegrationEvents.RemoveRange(dbContext.IntegrationEvents);
        dbContext.SaveChanges();

        _applicationFactory.Dispose();
    }
}
