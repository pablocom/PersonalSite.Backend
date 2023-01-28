using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PersonalSite.Domain;
using PersonalSite.Persistence;
using Xunit;

namespace PersonalSite.IntegrationTests;

[Collection(nameof(PersonalSiteIntegrationTestBase))]
public abstract class PersonalSiteIntegrationTestBase : IAsyncDisposable
{
    private IDbContextTransaction _transaction;
    protected IJobExperienceRepository Repository { get; }
    protected PersonalSiteDbContext DbContext { get; }

    protected PersonalSiteIntegrationTestBase()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PersonalSiteDatabase");
        var options = new DbContextOptionsBuilder<PersonalSiteDbContext>()
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging()
            .Options;

        DbContext = new PersonalSiteDbContext(options);
        Repository = new JobExperienceRepository(DbContext);

        _transaction = DbContext.Database.BeginTransaction();
    }

    protected async Task SaveChangesAndClearTracking()
    {
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
    }

    public async ValueTask DisposeAsync()
    {
        await _transaction.RollbackAsync();
    }
}
