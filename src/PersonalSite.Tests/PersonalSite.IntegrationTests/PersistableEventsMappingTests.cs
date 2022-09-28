using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalSite.Domain;
using PersonalSite.Persistence.Events;

namespace PersonalSite.IntegrationTests;

[TestFixture]
public class PersistableEventsMappingTests : PersonalSiteIntegrationTestBase
{
    [Test]
    public async Task IsPersistedAsync()
    {
        var createdAt = new DateTime(2022, 2, 22, 1, 2, 3, 234, DateTimeKind.Utc);
        var serializedEventData = JsonSerializer.Serialize(new DummyEvent(1, "text", new DateTime(2022, 9, 22)));
        var id = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f");
        var persistableEvent = new PersistableEvent(
            id,
            typeof(DummyEvent).FullName,
            serializedEventData,
            createdAt
        );

        DbContext.PersistableEvents.Add(persistableEvent);
        CloseContext();

        var savedEvents = await DbContext.PersistableEvents.ToArrayAsync();
        Assert.That(savedEvents, Has.Length.EqualTo(1));
        Assert.That(savedEvents[0].Id, Is.EqualTo(id));
        Assert.That(savedEvents[0].FullyQualifiedTypeName, Is.EqualTo(typeof(DummyEvent).FullName));
        Assert.That(savedEvents[0].SerializedData, Is.EqualTo(serializedEventData));
        Assert.That(savedEvents[0].CreatedAt, Is.EqualTo(createdAt));
    }

    private class DummyEvent : IIntegrationEvent
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