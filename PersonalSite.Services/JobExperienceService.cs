using PersonalSite.Domain.Entities;
using PersonalSite.Domain.ValueObjects;
using PersonalSite.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalSite.Services
{
    public interface IJobExperienceService
    {
        void CreateJobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack);
        IEnumerable<JobExperience> GetAll();
    }

    public class JobExperienceService : IJobExperienceService
    {
        private readonly IPersonalSiteRepository repository;

        public JobExperienceService(IPersonalSiteRepository repository)
        {
            this.repository = repository;
        }

        public void CreateJobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack)
        {
            repository.Add(new JobExperience(company, description, new JobPeriod(jobPeriodStart, jobPeriodEnd), techStack));
        }

        public IEnumerable<JobExperience> GetAll()
        {
            return repository.GetAll<JobExperience>().ToArray();
        }
    }
}
