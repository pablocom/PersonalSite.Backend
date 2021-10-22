using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public void Add(JobExperience jobExperience)
        {
            context.JobExperiences.Add(jobExperience);
        }
        
        public IEnumerable<JobExperience> GetAllJobExperiences()
        {
            return context.JobExperiences.ToArray();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}