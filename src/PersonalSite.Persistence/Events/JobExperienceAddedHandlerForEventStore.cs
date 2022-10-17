using System.Text.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Domain.Events;
using System.Globalization;
using System.Text.Json.Serialization;
using PersonalSite.Domain;

namespace PersonalSite.Persistence.Events;

public class JobExperienceAddedHandlerForEventStore : INotificationHandler<JobExperienceAdded>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new DateOnlyJsonConverter() }
    };
    private readonly PersonalSiteDbContext _dbContext;
    private readonly IClock _clock;

    public JobExperienceAddedHandlerForEventStore(PersonalSiteDbContext dbContext, IClock clock)
    {
        _dbContext = dbContext;
        _clock = clock;
    }

    public Task Handle(JobExperienceAdded ev, CancellationToken cancellationToken)
    {
        var eventToPersist = new IntegrationEvent(
            Guid.NewGuid(), 
            ev.GetType().FullName, 
            JsonSerializer.Serialize(ev, JsonSerializerOptions), 
            _clock.UtcNow);

        _dbContext.Add(eventToPersist);
        return Task.CompletedTask;
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
