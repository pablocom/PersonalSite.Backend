using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.UnitTests;

public abstract class PersonalSiteDomainTestBase
{
    protected IJobExperienceRepository Repository { get; set; }
    private FakeInMemoryPersonalSiteDbContext DbContext { get; set; }

    protected PersonalSiteDomainTestBase()
    {
        DbContext = new FakeInMemoryPersonalSiteDbContext(new DbContextOptions<PersonalSiteDbContext>());
        DbContext.Database.EnsureDeleted();
        Repository = new FakeJobExperienceRepository(DbContext);
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
}
