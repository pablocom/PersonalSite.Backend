using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests;

public class FakeJobExperienceRepository : IJobExperienceRepository
{
    private readonly FakeInMemoryPersonalSiteDbContext _dbContext;

    public FakeJobExperienceRepository(FakeInMemoryPersonalSiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(JobExperience jobExperience)
    {
        await _dbContext.AddAsync(jobExperience);
    }

    public async Task<IEnumerable<JobExperience>> GetAllJobExperiences(CancellationToken cancellationToken)
    {
        return await _dbContext.JobExperiences.ToArrayAsync(cancellationToken);
    }
}
