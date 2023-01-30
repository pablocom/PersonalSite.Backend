using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using PersonalSite.Domain;
using PersonalSite.Domain.Events;
using PersonalSite.Persistence.Events;
using Xunit;

namespace PersonalSite.IntegrationTests.HandlersForEventStore;

public class WhenHandlingJobExperienceAddedForEventStore : PersonalSiteIntegrationTestBase
{
    private readonly IClock _clock;
    private static readonly DateTimeOffset Date = new(2022, 2, 5, 0, 0, 0, TimeSpan.Zero);
    
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new DateOnlyJsonConverter() },
        PropertyNameCaseInsensitive = true
    };

    public WhenHandlingJobExperienceAddedForEventStore()
    {
        _clock = Substitute.For<IClock>();
        _clock.UtcNow.Returns(Date);
    }

    [Fact]
    public async Task StoresEvent()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };
        var @event = new JobExperienceAdded(company, description, startDate, endDate, techStack);

        var handler = new JobExperienceAddedHandlerForEventStore(DbContext, _clock);
        await handler.Handle(@event, CancellationToken.None);
        await SaveChangesAndClearTracking();

        var savedEvents = await DbContext.IntegrationEvents.ToArrayAsync();
        Assert.Single(savedEvents);
        Assert.Equal(savedEvents[0].FullyQualifiedTypeName, typeof(JobExperienceAdded).FullName);
        Assert.Equal(savedEvents[0].SerializedData, JsonSerializer.Serialize(@event, JsonSerializerOptions));
        Assert.False(savedEvents[0].IsPublished);
        Assert.Equal(savedEvents[0].CreatedAt, Date);
    }

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString(), DateFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}
