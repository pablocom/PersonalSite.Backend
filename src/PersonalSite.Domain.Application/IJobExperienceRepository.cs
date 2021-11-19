using System.Collections.Generic;
using PersonalSite.WebApi;
using PersonalSite.WebApi.Model.JobExperienceAggregate;

namespace PersonalSite.Application;

public interface IJobExperienceRepository : IDomainRepository<JobExperience>
{
    void Add(JobExperience jobExperience);
    IEnumerable<JobExperience> GetAllJobExperiences();
}
