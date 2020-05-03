using System;

namespace PersonalSite.Services
{
    public interface IJobExperienceService
    {
        void CreateJobExperience(string company, string jobExperience, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack);
    }
}