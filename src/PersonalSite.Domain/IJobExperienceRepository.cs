using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain;

public interface IJobExperienceRepository
{
    Task Add(JobExperience jobExperience);
    Task<IEnumerable<JobExperience>> GetAllJobExperiences(CancellationToken cancellationToken = default);
}
