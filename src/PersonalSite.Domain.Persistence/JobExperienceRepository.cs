using PersonalSite.Application;
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

    public void Save(JobExperience jobExperience)
    {
        context.JobExperiences.Add(jobExperience);
    }

    public IEnumerable<JobExperience> GetAll()
    {
        return context.JobExperiences.ToArray();
    }
}
