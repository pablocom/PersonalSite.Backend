using System;
using System.Linq;
using NUnit.Framework;
using PersonalSite.Domain.Entities;
using PersonalSite.Services;
using PersonalSite.Tests.UnitTests.Builders;

namespace PersonalSite.Tests.UnitTests.Services
{
    public class WhenGettingJobExperiences : PersonalSiteTestBase
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

            var jobExperiences = Repository.GetAll<JobExperience>().ToArray();

            var firstJobExperience = jobExperiences[0];
            Assert.That(firstJobExperience.Company, Is.EqualTo(company));
            Assert.That(firstJobExperience.Description, Is.EqualTo(description));
            Assert.That(firstJobExperience.JobPeriod.Start, Is.EqualTo(startDate));
            Assert.That(firstJobExperience.JobPeriod.End, Is.EqualTo(endDate));
            CollectionAssert.AreEquivalent(techStack, firstJobExperience.TechStack);

            var secondJobExperience = jobExperiences[1];
            Assert.That(secondJobExperience.Company, Is.EqualTo(otherCompany));
            Assert.That(secondJobExperience.Description, Is.EqualTo(otherDescription));
            Assert.That(secondJobExperience.JobPeriod.Start, Is.EqualTo(otherStartDate));
            Assert.That(secondJobExperience.JobPeriod.End, Is.EqualTo(otherEndDate));
            CollectionAssert.AreEquivalent(otherTechStack, secondJobExperience.TechStack);
        }
    }
}