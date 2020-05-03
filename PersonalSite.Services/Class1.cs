using System;
using PersonalSite.Domain.Entities;
using PersonalSite.Domain.ValueObjects;
using PersonalSite.Persistence;

namespace PersonalSite.Services
{
    public class JobExperienceService : IJobExperienceService
    {
        private readonly IPersonalSiteRepository repository;

        public JobExperienceService(IPersonalSiteRepository repository)
        {
            this.repository = repository;
        }

        public void CreateJobExperience(string company, string jobExperience, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack)
        {
            this.repository.Add(new JobExperience(company, jobExperience, new JobPeriod(jobPeriodStart, jobPeriodEnd), techStack));
        }
    }
}
