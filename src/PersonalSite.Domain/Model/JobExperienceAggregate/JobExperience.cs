﻿using System.Collections.Generic;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.Extensions;

namespace PersonalSite.Domain.Model.JobExperienceAggregate
{
    public class JobExperience : Entity
    {
        public string Company { get; private set; }
        public string Description { get; private set; }
        public JobPeriod JobPeriod { get; private set; }
        public ICollection<string> TechStack { get; private set; }

        // EntityFramework required constructor
        protected JobExperience() 
        { }

        public JobExperience(string company, string description, JobPeriod jobPeriod, ICollection<string> techStack)
        {
            CheckCompanyAndDescriptionNotNullOrEmpty(company, description);

            Company = company;
            Description = description;
            JobPeriod = jobPeriod;
            TechStack = techStack;
        }

        private void CheckCompanyAndDescriptionNotNullOrEmpty(string company, string description)
        {
            if (company.IsNullOrEmpty() || description.IsNullOrEmpty())
                throw new DomainException("Job experience company and description must have value");
        }
    }
}
