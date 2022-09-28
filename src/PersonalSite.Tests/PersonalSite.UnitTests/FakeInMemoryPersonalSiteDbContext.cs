using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence.Mappings;

namespace PersonalSite.UnitTests;

public class FakeInMemoryPersonalSiteDbContext : DbContext
{
    public DbSet<JobExperience> JobExperiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "FakePersonalSiteDbContext");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyMappingOverrides(modelBuilder);
    }

    private static void ApplyMappingOverrides(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new JobExperienceMappingOverride());
    }
}
