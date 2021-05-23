using System.Collections.Generic;

namespace PersonalSite.Domain.Model.JobExperienceAggregate
{
    public interface IJobExperienceRepository : IDomainRepository<JobExperience>
    {
        void Add(JobExperience entity);
        IEnumerable<JobExperience> GetAllJobExperiences();
        void SaveChanges();
    }
}