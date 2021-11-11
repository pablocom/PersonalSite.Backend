using System.Collections.Generic;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.Application;

public interface IJobExperienceRepository : IDomainRepository<JobExperience>
{
    void Add(JobExperience jobExperience);
    IEnumerable<JobExperience> GetAllJobExperiences();
}
