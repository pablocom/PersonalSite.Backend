﻿using System.Collections.Generic;

namespace PersonalSite.Domain
{
    public class JobExperience : Entity
    {
        public string Company { get; private set; }
        public string Description { get; private set; }
        public JobPeriod JobPeriod { get; private set; }
        public ICollection<string> TechStack { get; private set; }

        public JobExperience(string company, string description, JobPeriod jobPeriod, ICollection<string> techStack)
        {
            Company = company;
            Description = description;
            JobPeriod = jobPeriod;
            TechStack = techStack;
        }
    }
}
