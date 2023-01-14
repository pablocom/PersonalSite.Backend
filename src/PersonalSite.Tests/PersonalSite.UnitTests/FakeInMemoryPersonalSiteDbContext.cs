using Microsoft.EntityFrameworkCore;
using PersonalSite.Persistence;

namespace PersonalSite.UnitTests;

public class FakeInMemoryPersonalSiteDbContext : PersonalSiteDbContext
{
    public FakeInMemoryPersonalSiteDbContext(DbContextOptions<PersonalSiteDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "FakePersonalSiteDbContext");
    }
}
