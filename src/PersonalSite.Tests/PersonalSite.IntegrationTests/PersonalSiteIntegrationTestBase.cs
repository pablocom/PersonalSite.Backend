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
    protected IJobExperienceRepository Repository;
    private PersonalSiteDbContext _dbContext;

    [SetUp]
    protected void Setup()
    {
        var connectionString = Environment.GetEnvironmentVariable("PersonalSiteConnectionString");
        var options = new DbContextOptionsBuilder<PersonalSiteDbContext>()
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging()
            .Options;

        _dbContext = new PersonalSiteDbContext(options);
        Repository = new JobExperienceRepository(_dbContext);
        _transaction = _dbContext.Database.BeginTransaction();

        AdditionalSetup();
    }

    protected void CloseContext() => _dbContext.SaveChanges();

    protected virtual void AdditionalSetup() { }

    [TearDown]
    protected void Teardown()
    {
        _transaction.Rollback();
    }
}
