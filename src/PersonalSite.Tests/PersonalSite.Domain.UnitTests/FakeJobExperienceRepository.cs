using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public void Add(JobExperience entity)
        {
            dbContext.Add(entity);
        }
        
        public IEnumerable<JobExperience> GetAllJobExperiences()
        {
            return dbContext.JobExperiences.ToArray();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}