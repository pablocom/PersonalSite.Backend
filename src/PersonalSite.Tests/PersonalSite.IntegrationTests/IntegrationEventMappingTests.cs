using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Persistence.Events;
using Xunit;

namespace PersonalSite.IntegrationTests;

public class IntegrationEventMappingTests : PersonalSiteIntegrationTestBase
{
    [Fact]
    public async Task IsPersisted()
    {
        var createdAt = new DateTimeOffset(2022, 2, 22, 1, 2, 3, 234, TimeSpan.Zero);
        var serializedEventData = JsonSerializer.Serialize(new DummyEvent(1, "text", new DateTime(2022, 9, 22)));
        var id = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f");
        var integrationEvent = new IntegrationEvent(
            id,
            typeof(DummyEvent).FullName,
            serializedEventData,
            createdAt
        );

        DbContext.IntegrationEvents.Add(integrationEvent);
        CloseContext();

        var savedEvents = await DbContext.IntegrationEvents.ToArrayAsync();
        Assert.Single(savedEvents);
        Assert.Equal(savedEvents[0].Id, id);
        Assert.Equal(savedEvents[0].FullyQualifiedTypeName, typeof(DummyEvent).FullName);
        Assert.Equal(savedEvents[0].SerializedData, serializedEventData);
        Assert.Equal(savedEvents[0].CreatedAt, createdAt);
    }

    private class DummyEvent 
    {
        public int SomeId { get; set; }
        public string SomeText { get; set; }
        public DateTime SomeDate { get; set; }

        public DummyEvent(int someId, string someText, DateTime someDate)
        {
            SomeId = someId;
            SomeText = someText;
            SomeDate = someDate;
        }
    }
}