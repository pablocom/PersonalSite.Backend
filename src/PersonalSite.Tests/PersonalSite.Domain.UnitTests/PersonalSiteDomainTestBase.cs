using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests;

public class PersonalSiteDomainTestBase
{
    protected IJobExperienceRepository Repository { get; set; }
    private FakeInMemoryPersonalSiteDbContext DbContext { get; set; }

    [SetUp]
    public void SetUp()
    {
        DomainEvents.Init();

        DbContext = new FakeInMemoryPersonalSiteDbContext();
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
