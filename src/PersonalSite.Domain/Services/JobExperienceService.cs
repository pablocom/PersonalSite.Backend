using System;
using System.Linq;
using PersonalSite.Domain.Dtos;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.Services
{
    public interface IJobExperienceService
    {
        void CreateJobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack);
        JobExperienceDto[] GetJobExperiences();
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

        public JobExperienceDto[] GetJobExperiences()
        {
            var jobExperiences = repository.GetAllJobExperiences().ToArray();
            return jobExperiences.Select(JobExperienceDto.From).ToArray();
        }
    }
}
