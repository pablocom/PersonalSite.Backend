using System;

namespace PersonalSite.Application.Dtos
{
    public class JobExperienceCreationArgs
    {
        public string Company { get; }
        public string Description { get; }
        public DateTime JobPeriodStart { get; }
        public DateTime? JobPeriodEnd { get; }
        public string[] TechStack { get; }

        public JobExperienceCreationArgs(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack)
        {
            Company = company;
            Description = description;
            JobPeriodStart = jobPeriodStart;
            JobPeriodEnd = jobPeriodEnd;
            TechStack = techStack;
        }
    }
}
