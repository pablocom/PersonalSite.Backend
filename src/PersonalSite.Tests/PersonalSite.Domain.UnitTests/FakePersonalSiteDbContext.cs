﻿using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence.Mappings;

namespace PersonalSite.UnitTests;

public class FakePersonalSiteDbContext : DbContext
{
    public DbSet<JobExperience> JobExperiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "FakePersonalSiteDbContext");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        AddPersonalSiteMappings(modelBuilder);
    }

    private static void AddPersonalSiteMappings(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new JobExperienceMappingOverride());
    }
}
