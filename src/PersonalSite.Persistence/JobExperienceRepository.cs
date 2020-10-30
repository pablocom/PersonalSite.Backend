using System;
using System.Linq;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Persistence
{
    public class JobExperienceRepository : IJobExperienceRepository
    {
        private readonly PersonalSiteDbContext _context;

        public JobExperienceRepository(PersonalSiteDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(JobExperience entity)
        {
            _context.Set<JobExperience>().Add(entity);
        }
        
        public IQueryable<JobExperience> GetAllJobExperiences()
        {
            return _context.Set<JobExperience>().AsQueryable();
        }
    }
}