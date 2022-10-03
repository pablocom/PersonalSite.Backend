using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.UnitTests;

public class PersonalSiteDomainTestBase
{
    protected IJobExperienceRepository Repository { get; set; }
    private FakeInMemoryPersonalSiteDbContext DbContext { get; set; }

    [SetUp]
    public void SetUp()
    {
        DbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());
        DbContext.Database.EnsureDeleted();
        Repository = new FakeJobExperienceRepository(DbContext);

        AdditionalSetup();
    }

    protected void CloseContext()
    {
        DbContext.SaveChanges();
    }

    protected async Task AssumeDataInRepository(params JobExperience[] entities)
    {
        foreach (var entity in entities)
            await Repository.Add(entity);

        CloseContext();
    }

    protected virtual void AdditionalSetup() { }
}
