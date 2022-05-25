using System.Collections.Generic;
using System.Linq;
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

    public void Add(JobExperience jobExperience)
    {
        _dbContext.Add(jobExperience);
    }

    public IEnumerable<JobExperience> GetAllJobExperiences()
    {
        return _dbContext.JobExperiences.ToArray();
    }
}
