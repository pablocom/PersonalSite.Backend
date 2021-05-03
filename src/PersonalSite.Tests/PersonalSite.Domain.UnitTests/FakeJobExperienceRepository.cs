using System.Linq;
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
        
        public IQueryable<JobExperience> GetAllJobExperiences()
        {
            return dbContext.Set<JobExperience>().AsQueryable();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}