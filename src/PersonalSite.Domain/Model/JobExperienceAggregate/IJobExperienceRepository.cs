using System.Linq;

namespace PersonalSite.Domain.Model.JobExperienceAggregate
{
    public interface IJobExperienceRepository : IDomainRepository<JobExperience>
    {
        void Add(JobExperience entity);
        IQueryable<JobExperience> GetAllJobExperiences();
    }
}