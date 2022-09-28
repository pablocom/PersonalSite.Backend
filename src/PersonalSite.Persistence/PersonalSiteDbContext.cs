using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence.Events;
using PersonalSite.Persistence.Mappings;

namespace PersonalSite.Persistence;

public class PersonalSiteDbContext : DbContext
{
    public PersonalSiteDbContext(DbContextOptions<PersonalSiteDbContext> options)
        : base(options)
    { }

    public DbSet<PersistableEvent> PersistableEvents { get; set; }
    public DbSet<JobExperience> JobExperiences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new JobExperienceMappingOverride());
        modelBuilder.ApplyConfiguration(new PersistableEventMappingOverride());
    }

    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        //var domainEvents = ChangeTracker.Entries<Entity>()
        //    .SelectMany(po => po.Entity.DomainEvents)
        //    .ToArray();

        //var events = domainEvents
        //    .Where(ev => ev as IIntegrationEvent is not null)
        //    .Select(ev => new PersistableEvent(ev.GetType(), JsonSerializer.Serialize(ev), DateTime.UtcNow))
        //    .ToArray();

        //PersistableEvents.AddRange(events);

        return base.SaveChangesAsync(cancellationToken);
    }
}
