using NUnit.Framework;
using PersonalSite.Domain.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.UnitTests;

public class PersonalSiteDomainTestBase
{
    protected IJobExperienceRepository Repository { get; set; }
    private FakePersonalSiteDbContext DbContext { get; set; }

    [SetUp]
    public void SetUp()
    {
        DbContext = new FakePersonalSiteDbContext();
        DbContext.Database.EnsureDeleted();
        Repository = new FakeJobExperienceRepository(DbContext);

        AdditionalSetup();
    }

    protected void CloseContext()
    {
        DbContext.SaveChanges();
    }

    protected void AssumeDataInRepository(params JobExperience[] entities)
    {
        foreach (var entity in entities)
            Repository.Add(entity);

        CloseContext();
    }

    protected virtual void AdditionalSetup() { }
}
