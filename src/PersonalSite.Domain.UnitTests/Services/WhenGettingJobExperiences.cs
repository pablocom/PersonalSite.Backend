using System;
using System.Linq;
using NUnit.Framework;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Domain.UnitTests.Builders;

namespace PersonalSite.Domain.UnitTests.Services
{
    public class WhenGettingJobExperiences : PersonalSiteDomainTestBase
    {
        private IJobExperienceService service;

        protected override void AdditionalSetup()
        {
            service = new JobExperienceService(Repository);
        }

        [Test]
        public void MultipleJobExperiencesAreReturned()
        {
            var company = "Ryanair";
            var description = "Software Engineer";
            var startDate = new DateTime(2019, 09, 09);
            var endDate = new DateTime(2021, 07, 01);
            var techStack = new[] { ".Net", "MySQL" };

            var otherCompany = "1millionBot";
            var otherDescription = "Web Developer";
            var otherStartDate = new DateTime(2018, 02, 09);
            var otherEndDate = new DateTime(2018, 09, 01);
            var otherTechStack = new[] { "Node.js", "MongoDB" };

            AssumeDataInRepository(new[]
            {
                new JobExperienceBuilder()
                    .WithCompany(company)
                    .WithDescription(description)
                    .WithStartDate(startDate)
                    .WithEndDate(endDate)
                    .WithTechStack(techStack)
                    .Build(),
                new JobExperienceBuilder()
                    .WithCompany(otherCompany)
                    .WithDescription(otherDescription)
                    .WithStartDate(otherStartDate)
                    .WithEndDate(otherEndDate)
                    .WithTechStack(otherTechStack)
                    .Build()
            });

            var jobExperiences = service.GetJobExperiences().ToArray();

            Assert.That(jobExperiences.Length, Is.EqualTo(2));
            AssertJobExperience(jobExperiences[0], company, description, startDate, endDate, techStack);
            AssertJobExperience(jobExperiences[1], otherCompany, otherDescription, otherStartDate, otherEndDate, otherTechStack);
        }

        private static void AssertJobExperience(JobExperience jobExperience, string company, string description,
            DateTime startDate, DateTime endDate, string[] techStack)
        {
            Assert.That(jobExperience.Company, Is.EqualTo(company));
            Assert.That(jobExperience.Description, Is.EqualTo(description));
            Assert.That(jobExperience.JobPeriod.Start, Is.EqualTo(startDate));
            Assert.That(jobExperience.JobPeriod.End, Is.EqualTo(endDate));
            CollectionAssert.AreEquivalent(techStack, jobExperience.TechStack);
        }
    }
}