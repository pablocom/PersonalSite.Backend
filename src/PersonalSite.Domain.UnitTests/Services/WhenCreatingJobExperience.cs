using System;
using System.Linq;
using NUnit.Framework;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Services;

namespace PersonalSite.Domain.UnitTests.Services
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

            var jobExperience = Repository.GetAllJobExperiences().Single();
            AssertJobExperience(jobExperience, company, description, startDate, endDate, techStack);
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(null, "")]
        [TestCase("", "")]
        public void RaisesArgumentExceptionIfCompanyOrDescriptionIsNullOrWhiteSpace(string company, string description)
        {
            TestDelegate action = () => 
            {
                service.CreateJobExperience(company, description, new DateTime(), new DateTime(), new string[0]);
            };
            
            var exception = Assert.Throws<DomainException>(action);

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