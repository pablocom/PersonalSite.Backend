using Microsoft.EntityFrameworkCore;
using PersonalSite.Persistence;

namespace PersonalSite.IntegrationEventsPublishWorker.UnitTests;

public class FakeInMemoryPersonalSiteDbContext : PersonalSiteDbContext
{
    public FakeInMemoryPersonalSiteDbContext(DbContextOptions<PersonalSiteDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
    }
}