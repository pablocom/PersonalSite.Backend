using System;
using System.Linq;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.Services
{
    public interface IJobExperienceService
    {
        void CreateJobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack);
        JobExperience[] GetJobExperiences();
    }

    public class JobExperienceService : IJobExperienceService
    {
        private readonly IJobExperienceRepository repository;
        
        public JobExperienceService(IJobExperienceRepository repository)
        {
            this.repository = repository;
        }

        public void CreateJobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack)
        {
            repository.Add(new JobExperience(company, description, jobPeriodStart, jobPeriodEnd, techStack));
        }

        public JobExperience[] GetJobExperiences()
        {
            return repository.GetAllJobExperiences().ToArray();
        }
    }
}
