using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.Domain.IntegrationTests
{
    public class PersonalSiteIntegrationTestBase
    {
        private IDbContextTransaction transaction;
        protected IJobExperienceRepository Repository;
        private PersonalSiteDbContext dbContext;

        [SetUp]
        protected void Setup()
        {
            var connectionString = Environment.GetEnvironmentVariable("PersonalSiteConnectionString");
            var options = new DbContextOptionsBuilder<PersonalSiteDbContext>()
                .UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .Options;
            
            dbContext = new PersonalSiteDbContext(options); 
            Repository = new JobExperienceRepository(dbContext);
            this.transaction = dbContext.Database.BeginTransaction();

            AdditionalSetup();
        }

        protected void CloseContext()
        {
            Repository.SaveChanges();
        }
        
        protected virtual void AdditionalSetup() { }

        [TearDown]
        protected void Teardown()
        {
            transaction.Rollback();
        }
    }
}