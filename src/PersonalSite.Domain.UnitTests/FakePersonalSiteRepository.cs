using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Persistence;

namespace PersonalSite.Domain.UnitTests
{
    public class FakePersonalSiteRepository : IJobExperienceRepository
    {
        private PersonalSiteFakeDbContext dbContext;

        public FakePersonalSiteRepository(PersonalSiteFakeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(JobExperience entity)
        {
            dbContext.Add(entity);
        }
        
        public IQueryable<JobExperience> GetAllJobExperiences()
        {
            return dbContext.Set<JobExperience>().AsQueryable();
        }
    }
}