using System;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.UnitTests.Builders
{
    public class JobExperienceBuilder
    {
        private string company;
        private string description;
        private string[] techStack = new string[0];
        private DateTime startDate;
        private DateTime endDate;

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

        public static JobExperienceBuilder ValidJobExperience()
        {
            return new JobExperienceBuilder().WithCompany("SomeCompany").WithDescription("SomeDescription");
        }
        
        public JobExperience Build()
        {
            return new JobExperience(company, description, startDate, endDate, techStack);
        }
    }
}