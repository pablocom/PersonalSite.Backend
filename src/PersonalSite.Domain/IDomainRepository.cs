namespace PersonalSite.Domain
{
    /// <summary>
    /// Marks domain repository of an aggregate root
    /// </summary>
    /// <typeparam name="TAggregateRoot">Type of aggregate root of repository</typeparam>
    public interface IDomainRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    { }
}