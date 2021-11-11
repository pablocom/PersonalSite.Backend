using PersonalSite.Domain.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalSite.Persistence;

public class JobExperienceRepository : IJobExperienceRepository
{
    private readonly PersonalSiteDbContext context;

    public JobExperienceRepository(PersonalSiteDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(JobExperience jobExperience)
    {
        context.JobExperiences.Add(jobExperience);
    }

    public IEnumerable<JobExperience> GetAllJobExperiences()
    {
        return context.JobExperiences.ToArray();
    }
}
