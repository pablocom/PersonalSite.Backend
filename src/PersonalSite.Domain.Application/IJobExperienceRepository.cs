using PersonalSite.Domain;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System.Collections.Generic;

namespace PersonalSite.Application;

public interface IJobExperienceRepository : IDomainRepository<JobExperience>
{
    void Save(JobExperience jobExperience);
    IEnumerable<JobExperience> GetAll();
}
