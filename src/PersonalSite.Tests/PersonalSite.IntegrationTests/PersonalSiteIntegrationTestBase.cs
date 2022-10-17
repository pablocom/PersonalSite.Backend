using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Persistence;

namespace PersonalSite.IntegrationTests;

public class PersonalSiteIntegrationTestBase
{
    private IDbContextTransaction _transaction;
    protected IJobExperienceRepository Repository { get; private set; }
    protected PersonalSiteDbContext DbContext { get; private set; }

    [SetUp]
    protected void Setup()
    {
        var connectionString = Environment.GetEnvironmentVariable("PersonalSiteConnectionString");
        var options = new DbContextOptionsBuilder<PersonalSiteDbContext>()
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging()
            .Options;

        DbContext = new PersonalSiteDbContext(options);
        Repository = new JobExperienceRepository(DbContext);
        _transaction = DbContext.Database.BeginTransaction();

        AdditionalSetup();
    }

    protected void CloseContext() => DbContext.SaveChanges();

    protected virtual void AdditionalSetup() { }

    [TearDown]
    protected void Teardown()
    {
        _transaction.Rollback();
    }
}
