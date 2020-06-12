using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence;

namespace PersonalSite.Tests.UnitTests
{
    public class PersonalSiteTestBase
    {
        protected IPersonalSiteRepository Repository { get; set; }
        protected PersonalSiteFakeDbContext FakeDbContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            FakeDbContext = new PersonalSiteFakeDbContext();
            FakeDbContext.Database.EnsureDeleted();
            Repository = new FakePersonalSiteRepository(FakeDbContext);

            AdditionalSetup();
        }

        public void CloseContext()
        {
            FakeDbContext.SaveChanges();
        }

        protected void AssumeDataInRepository<TEntity>(params TEntity[] entities) where TEntity : Entity
        {
            Repository.AddAll(entities);
            CloseContext();
        }

        protected virtual void AdditionalSetup() { }
    }

    
}