using NUnit.Framework;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.Domain.UnitTests
{
    public class PersonalSiteTestBase
    {
        protected IJobExperienceRepository Repository { get; set; }
        private PersonalSiteFakeDbContext FakeDbContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            FakeDbContext = new PersonalSiteFakeDbContext();
            FakeDbContext.Database.EnsureDeleted();
            Repository = new FakePersonalSiteRepository(FakeDbContext);

            AdditionalSetup();
        }

        protected void CloseContext()
        {
            FakeDbContext.SaveChanges();
        }

        protected void AssumeDataInRepository(params JobExperience[] entities)
        {
            foreach (var entity in entities) 
                Repository.Add(entity);
            
            CloseContext();
        }

        protected virtual void AdditionalSetup() { }
    }
}