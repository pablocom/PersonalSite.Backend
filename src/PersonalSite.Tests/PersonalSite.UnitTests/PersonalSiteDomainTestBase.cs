using NSubstitute;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Domain.Events;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.IoC;
using System;

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

    protected void AssumeDataInRepository(params JobExperience[] entities)
    {
        foreach (var entity in entities)
            Repository.Add(entity);

        CloseContext();
    }

    protected IHandleDomainEventsSynchronouslyInCurrentScope<TDomainEvent> AssumeDomainEventHandlerWasRegistered<TDomainEvent>() 
        where TDomainEvent : IDomainEvent
    {
        var domainEventHandler = Substitute.For<IHandleDomainEventsSynchronouslyInCurrentScope<TDomainEvent>>();
        
        DomainEvents.RegisterSyncHandler(domainEventHandler.GetType());
        DependencyInjectionContainer.Current.GetService(domainEventHandler.GetType()).Returns(domainEventHandler);

        return domainEventHandler;
    }

    protected virtual void AdditionalSetup() { }
}
