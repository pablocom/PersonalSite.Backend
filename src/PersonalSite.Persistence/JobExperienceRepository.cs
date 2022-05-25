using PersonalSite.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalSite.Persistence;

public class JobExperienceRepository : IJobExperienceRepository
{
    private readonly PersonalSiteDbContext _context;

    public JobExperienceRepository(PersonalSiteDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(JobExperience jobExperience)
    {
        _context.JobExperiences.Add(jobExperience);
    }

    public IEnumerable<JobExperience> GetAllJobExperiences()
    {
        return _context.JobExperiences.ToArray();
    }
}
