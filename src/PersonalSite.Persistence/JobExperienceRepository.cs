using PersonalSite.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace PersonalSite.Persistence;

public class JobExperienceRepository : IJobExperienceRepository
{
    private readonly PersonalSiteDbContext _context;

    public JobExperienceRepository(PersonalSiteDbContext context)
    {
        _context = context;
    }

    public async Task Add(JobExperience jobExperience)
    {
        await _context.JobExperiences.AddAsync(jobExperience);
    }

    public async Task<IEnumerable<JobExperience>> GetAllJobExperiences(CancellationToken cancellationToken)
    {
        return await _context.JobExperiences.ToArrayAsync(cancellationToken);
    }
}
