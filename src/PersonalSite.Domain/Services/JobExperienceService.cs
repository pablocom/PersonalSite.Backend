using System;
using System.Linq;

namespace PersonalSite.Domain.Model.JobExperienceAggregate
{
    public interface IJobExperienceService
    {
        void CreateJobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack);
        JobExperience[] GetJobExperiences();
    }

    public class JobExperienceService : IJobExperienceService
    {
        private readonly IJobExperienceRepository _repository;
        
        public JobExperienceService(IJobExperienceRepository repository)
        {
            _repository = repository;
        }

        public void CreateJobExperience(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack)
        {
            _repository.Add(new JobExperience(company, description, jobPeriodStart, jobPeriodEnd, techStack));
        }

        public JobExperience[] GetJobExperiences()
        {
            return _repository.GetAllJobExperiences().ToArray();
        }
    }
}
