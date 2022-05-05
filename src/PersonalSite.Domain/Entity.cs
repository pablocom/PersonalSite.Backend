using System;

namespace PersonalSite.Domain;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }

    public bool Equals(Entity<TId> other)
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
        
        return Equals((Entity<TId>) other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
