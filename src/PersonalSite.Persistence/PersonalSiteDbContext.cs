using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence.Events;
using PersonalSite.Persistence.Mappings;

namespace PersonalSite.Persistence;

public class PersonalSiteDbContext : DbContext
{
    public DbSet<IntegrationEvent> IntegrationEvents { get; set; }
    public DbSet<JobExperience> JobExperiences { get; set; }

    public PersonalSiteDbContext(DbContextOptions<PersonalSiteDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new JobExperienceMappingOverride());
        modelBuilder.ApplyConfiguration(new IntegrationEventMappingOverride());
    }
}
