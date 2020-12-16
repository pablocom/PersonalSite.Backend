using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.Domain.IntegrationTests
{
    public class PersonalSiteIntegrationTestBase
    {
        protected IJobExperienceRepository Repository;
        private PersonalSiteDbContext dbContext;

        protected void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        [SetUp]
        protected void Setup()
        {
            var connectionString = Environment.GetEnvironmentVariable("PostgreSQLConnectionString");
            var options = new DbContextOptionsBuilder<PersonalSiteDbContext>()
                .UseNpgsql(connectionString ?? "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=personal_site_db;")
                .EnableSensitiveDataLogging()
                .Options;
            
            dbContext = new PersonalSiteDbContext(options); 
            Repository = new JobExperienceRepository(dbContext);

            AdditionalSetup();
        }

        protected virtual void AdditionalSetup() { }

        [TearDown]
        protected void Teardown()
        {
            dbContext.JobExperiences.RemoveRange(dbContext.JobExperiences);
            dbContext.SaveChanges();
        }
    }
}