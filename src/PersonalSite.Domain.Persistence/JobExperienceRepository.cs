using System;
using System.Linq;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Persistence
{
    public class JobExperienceRepository : IJobExperienceRepository
    {
        private readonly PersonalSiteDbContext context;

        public JobExperienceRepository(PersonalSiteDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(JobExperience entity)
        {
            context.Set<JobExperience>().Add(entity);
        }
        
        public IQueryable<JobExperience> GetAllJobExperiences()
        {
            return context.Set<JobExperience>().AsQueryable();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}