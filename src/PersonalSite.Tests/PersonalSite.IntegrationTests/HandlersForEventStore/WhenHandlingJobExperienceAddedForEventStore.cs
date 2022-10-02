using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalSite.Domain.Events;
using PersonalSite.Persistence.Events;

namespace PersonalSite.IntegrationTests.HandlersForEventStore;

public class WhenHandlingJobExperienceAddedForEventStore : PersonalSiteIntegrationTestBase
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new DateOnlyJsonConverter() },
        PropertyNameCaseInsensitive = true
    };

    [Test]
    public async Task StoresEvent()
    {
        var company = "Ryanair";
        var description = "Software Engineer";
        var startDate = new DateOnly(2019, 09, 09);
        var endDate = new DateOnly(2021, 07, 01);
        var techStack = new[] { ".Net", "MySQL" };
        var @event = new JobExperienceAdded(company, description, startDate, endDate, techStack);

        var handler = new JobExperienceAddedHandlerForEventStore(DbContext);
        await handler.Handle(@event, CancellationToken.None);
        CloseContext();

        var savedEvents = await DbContext.PersistableEvents.ToArrayAsync();
        Assert.That(savedEvents, Has.Length.EqualTo(1));
        Assert.That(savedEvents[0].FullyQualifiedTypeName, Is.EqualTo(typeof(JobExperienceAdded).FullName));
        Assert.That(savedEvents[0].SerializedData, Is.EqualTo(JsonSerializer.Serialize(@event, JsonSerializerOptions)));
    }

    private class DateOnlyJsonConverter : JsonConverter<DateOnly>
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
