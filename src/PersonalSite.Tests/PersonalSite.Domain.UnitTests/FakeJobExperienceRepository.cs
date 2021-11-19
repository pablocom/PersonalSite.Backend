using System.Collections.Generic;
using System.Linq;
using PersonalSite.Application;
using PersonalSite.WebApi.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests;

public class FakeJobExperienceRepository : IJobExperienceRepository
{
    private readonly FakePersonalSiteDbContext dbContext;

    public FakeJobExperienceRepository(FakePersonalSiteDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Add(JobExperience jobExperience)
    {
        dbContext.Add(jobExperience);
    }

    public IEnumerable<JobExperience> GetAllJobExperiences()
    {
        return dbContext.JobExperiences.ToArray();
    }
}
