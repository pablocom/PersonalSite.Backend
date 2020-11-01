using System;
using System.Linq;
using NUnit.Framework;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.IntegrationTests
{
    [TestFixture]
    public class WhenCreatingJobExperience : PersonalSiteIntegrationTestBase
    {
        private IJobExperienceService _service;

        protected override void AdditionalSetup()
        {
            _service = new JobExperienceService(Repository);
        }

        [Test]
        [Ignore("WIP: creating Integration Test project.")]
        public void JobExperienceIsPersisted()
        {
            var company = "Ryanair";
            var description = "Software Engineer";
            var startDate = new DateTime(2019, 09, 09);
            var endDate = new DateTime(2021, 07, 01);
            var techStack = new[] { ".Net", "MySQL" };

            _service.CreateJobExperience(company, description, startDate, endDate, techStack);
            SaveChanges();

            var jobExperience = Repository.GetAllJobExperiences().Single();
            AssertJobExperience(jobExperience, company, description, startDate, endDate, techStack);
        }
        
        private void AssertJobExperience(JobExperience jobExperience, string company, string description, DateTime startDate,
            DateTime endDate, string[] techStack)
        {
            Assert.That(jobExperience.Company, Is.EqualTo(company));
            Assert.That(jobExperience.Description, Is.EqualTo(description));
            Assert.That(jobExperience.JobPeriod.Start, Is.EqualTo(startDate));
            Assert.That(jobExperience.JobPeriod.End, Is.EqualTo(endDate));
            CollectionAssert.AreEquivalent(techStack, jobExperience.TechStack);
        }
    }
}