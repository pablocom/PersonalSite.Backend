using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
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

        protected virtual void AdditionalSetup() { }
    }

    
}