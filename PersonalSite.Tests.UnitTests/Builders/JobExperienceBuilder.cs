using PersonalSite.Domain.Entities;
using System;
using PersonalSite.Domain.ValueObjects;

namespace PersonalSite.Tests.UnitTests.Builders
{
    public class JobExperienceBuilder
    {
        private string company = "1million";
        private string description = "Software Engineer";
        private string[] techStack = new string[0];
        private DateTime startDate = new DateTime(2020, 1, 1);
        private DateTime endDate = new DateTime(2020, 5, 1);

        public JobExperienceBuilder WithCompany(string company)
        {
            this.company = company;
            return this;
        }

        public JobExperienceBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public JobExperienceBuilder WithTechStack(string[] techStack)
        {
            this.techStack = techStack;
            return this;
        }

        public JobExperienceBuilder WithStartDate(DateTime startDate)
        {
            this.startDate = startDate;
            return this;
        }
        
        public JobExperienceBuilder WithEndDate(DateTime endDate)
        {
            this.endDate = endDate;
            return this;
        }

        public JobExperience Build()
        {
            return new JobExperience(company, description, new JobPeriod(startDate, endDate), techStack);
        }
    }
}