using System.Collections.Generic;
using PersonalSite.Domain;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Application;

public interface IJobExperienceRepository
{
    void Add(JobExperience jobExperience);
    IEnumerable<JobExperience> GetAllJobExperiences();
}
