using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Persistence
{
    public interface IJobExperienceRepository
    {
        void Add(JobExperience entity);
        IQueryable<JobExperience> GetAllJobExperiences();
    }
}