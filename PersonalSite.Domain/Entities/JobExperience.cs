﻿using System.Collections.Generic;
using PersonalSite.Domain.Exceptions;
using PersonalSite.Domain.ValueObjects;

namespace PersonalSite.Domain.Entities
{
    public class JobExperience : Entity
    {
        public string Company { get; private set; }
        public string Description { get; private set; }
        public JobPeriod JobPeriod { get; private set; }
        public ICollection<string> TechStack { get; private set; }

        protected JobExperience()
        { }

        public JobExperience(string company, string description, JobPeriod jobPeriod, ICollection<string> techStack)
        {
            ValidateJobExperience(company, description);

            Company = company;
            Description = description;
            JobPeriod = jobPeriod;
            TechStack = techStack;
        }

        private void ValidateJobExperience(string company, string description)
        {
            if (company == null || description == null)
                throw new DomainException("Job experience company and description must have value");
        }
    }
}