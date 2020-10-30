using NUnit.Framework;
using PersonalSite.Domain.Entities;
using PersonalSite.Services;
using System;
using System.Linq;

namespace PersonalSite.Tests.UnitTests.Services
{
    [TestFixture]
    public class WhenCreatingJobExperience : PersonalSiteTestBase
    {
        private IJobExperienceService service;

        protected override void AdditionalSetup()
        {
            service = new JobExperienceService(Repository);
        }

        [Test]
        public void CreatesJobExperience()
        {
            var company = "Ryanair";
            var description = "Software Engineer";
            var startDate = new DateTime(2019, 09, 09);
            var endDate = new DateTime(2021, 07, 01);
            var techStack = new[] { ".Net", "MySQL" };

            service.CreateJobExperience(company, description, startDate, endDate, techStack);
            CloseContext();

            var jobExperience = Repository.GetAll<JobExperience>().Single();
            AssertJobExperience(jobExperience, company, description, startDate, endDate, techStack);
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(null, "")]
        [TestCase("", "")]
        public void RaisesArgumentExceptionIfCompanyOrDescriptionIsNullOrWhiteSpace(string company, string description)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                service.CreateJobExperience(company, description, new DateTime(), new DateTime(), new string[0]);
            });

            Assert.That(exception.Message, Is.EqualTo("Job experience company and description must have value"));
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