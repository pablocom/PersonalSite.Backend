using System.Collections.Generic;
using System.Linq;
using PersonalSite.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests;

public class FakeJobExperienceRepository : IJobExperienceRepository
{
    private readonly FakePersonalSiteDbContext dbContext;

    public FakeJobExperienceRepository(FakePersonalSiteDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Save(JobExperience jobExperience)
    {
        dbContext.Add(jobExperience);
    }

    public IEnumerable<JobExperience> Find()
    {
        return dbContext.JobExperiences.ToArray();
    }
}
