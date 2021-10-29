using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.UnitTests
{
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
}