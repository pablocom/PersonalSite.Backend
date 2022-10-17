﻿using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.UnitTests;

public class FakeInMemoryPersonalSiteDbContext : PersonalSiteDbContext
{
    public FakeInMemoryPersonalSiteDbContext(DbContextOptions<PersonalSiteDbContext> options) : base(options)
    {
    }

    public DbSet<JobExperience> JobExperiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "FakePersonalSiteDbContext");
    }
}
