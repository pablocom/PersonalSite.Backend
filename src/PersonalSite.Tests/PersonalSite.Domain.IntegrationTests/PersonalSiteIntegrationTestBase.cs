using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.Domain.IntegrationTests
{
    public class PersonalSiteIntegrationTestBase
    {
        protected IJobExperienceRepository Repository;
        protected PersonalSiteDbContext DbContext;

        protected void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        [SetUp]
        protected void Setup()
        {
            var options = new DbContextOptionsBuilder<PersonalSiteDbContext>()
                .UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=personal_site_db;")
                .EnableSensitiveDataLogging()
                .Options;
            DbContext = new PersonalSiteDbContext(options); 
            Repository = new JobExperienceRepository(DbContext);

            AdditionalSetup();
        }

        protected virtual void AdditionalSetup() { }

        [TearDown]
        protected void Teardown()
        {
            DbContext.JobExperiences.RemoveRange(DbContext.JobExperiences);
            DbContext.SaveChanges();
        }
    }
}