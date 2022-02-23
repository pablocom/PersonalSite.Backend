using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Persistence;
using System;
using PersonalSite.Domain.Events;

namespace PersonalSite.IntegrationTests;

public class PersonalSiteIntegrationTestBase
{
    private IDbContextTransaction transaction;
    protected IJobExperienceRepository Repository;
    private PersonalSiteDbContext dbContext;

    [SetUp]
    protected void Setup()
    {
        var connectionString = Environment.GetEnvironmentVariable("PersonalSiteConnectionString");
        var options = new DbContextOptionsBuilder<PersonalSiteDbContext>()
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging()
            .Options;

        dbContext = new PersonalSiteDbContext(options);
        Repository = new JobExperienceRepository(dbContext);
        transaction = dbContext.Database.BeginTransaction();

        AdditionalSetup();
    }

    protected void CloseContext() => dbContext.SaveChanges();

    protected virtual void AdditionalSetup() { }

    [TearDown]
    protected void Teardown()
    {
        transaction.Rollback();
    }
}
