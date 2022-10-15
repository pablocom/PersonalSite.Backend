using System;

namespace PersonalSite.Persistence.Events;

public class IntegrationEvent
{
    public Guid Id { get; protected set; }
    public string FullyQualifiedTypeName { get; protected set; }
    public string SerializedData { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public bool IsPublished { get; protected set; }

    protected IntegrationEvent()
    {
    }

    public IntegrationEvent(Guid id, string fullyQualifiedTypeName, string serializedData, DateTimeOffset createdAt)
    {
        Id = id;
        FullyQualifiedTypeName = fullyQualifiedTypeName;
        SerializedData = serializedData;
        CreatedAt = createdAt;
        IsPublished = false;
    }

    public void MarkAsProcessed()
    {
        IsPublished = true;
    }
}
