using System;

namespace PersonalSite.Persistence.Events;

public class PersistableEvent
{
    public Guid Id { get; protected set; }
    public string FullyQualifiedTypeName { get; protected set; }
    public string SerializedData { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public bool IsProcessed { get; protected set; }

    protected PersistableEvent()
    {
    }

    public PersistableEvent(Guid id, string fullyQualifiedTypeName, string serializedData, DateTime createdAt)
    {
        Id = id;
        FullyQualifiedTypeName = fullyQualifiedTypeName;
        SerializedData = serializedData;
        CreatedAt = createdAt;
        IsProcessed = false;
    }

    public void MarkAsProcessed()
    {
        IsProcessed = true;
    }
}
