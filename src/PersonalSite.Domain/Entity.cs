using System;
using System.Collections.Generic;

namespace PersonalSite.Domain;

public abstract class Entity : IEquatable<Entity>
{
    public IList<IDomainEvent> DomainEvents { get; } = new List<IDomainEvent>();
    public int Id { get; protected set; }

    public bool Equals(Entity other)
    {
        if (ReferenceEquals(null, other)) 
            return false;
        if (ReferenceEquals(this, other)) 
            return true;
        
        return Id.Equals(other.Id);
    }

    public override bool Equals(object other)
    {
        if (ReferenceEquals(null, other)) 
            return false;
        if (ReferenceEquals(this, other)) 
            return true;
        if (other.GetType() != GetType()) 
            return false;
        
        return Equals((Entity) other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
}
