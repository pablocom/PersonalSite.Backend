using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain;
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
}
