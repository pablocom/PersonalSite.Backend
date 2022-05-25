using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalSite.Domain;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Application;

public interface IJobExperienceRepository
{
    Task Add(JobExperience jobExperience);
    Task<IEnumerable<JobExperience>> GetAllJobExperiences();
}
