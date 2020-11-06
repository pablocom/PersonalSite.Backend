namespace PersonalSite.Domain
{
    /// <summary>
    /// Marks domain repository of an aggregate root
    /// </summary>
    /// <typeparam name="T">Type of aggregate root of repository</typeparam>
    public interface IDomainRepository<T> where T : IAggregateRoot
    { }
}