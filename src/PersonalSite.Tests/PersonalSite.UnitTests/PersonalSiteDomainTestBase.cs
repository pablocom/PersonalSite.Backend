using NSubstitute;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.IoC;
using System;
using System.Threading.Tasks;

namespace PersonalSite.UnitTests;

public class PersonalSiteDomainTestBase
{
    private static readonly IServiceProviderProxy ServiceProvider = Substitute.For<IServiceProviderProxy>();
    protected IJobExperienceRepository Repository { get; set; }
    private FakeInMemoryPersonalSiteDbContext DbContext { get; set; }

    [SetUp]
    public void SetUp()
    {
        DependencyInjectionContainer.Init(ServiceProvider);

        DbContext = new FakeInMemoryPersonalSiteDbContext();
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
