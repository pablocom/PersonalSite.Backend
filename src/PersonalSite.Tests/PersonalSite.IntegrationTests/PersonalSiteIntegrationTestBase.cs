using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PersonalSite.Domain;
using PersonalSite.Persistence;

namespace PersonalSite.IntegrationTests;

public class PersonalSiteIntegrationTestBase : IDisposable
{
    private IDbContextTransaction _transaction;
    protected IJobExperienceRepository Repository { get; private set; }
    protected PersonalSiteDbContext DbContext { get; private set; }

    public PersonalSiteIntegrationTestBase()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PersonalSiteDatabase");
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

    void IDisposable.Dispose()
    {
        _transaction.Rollback();
    }
}
