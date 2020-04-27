using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence.Mappings;

namespace PersonalSite.Persistence
{
    public class PersonalSiteDbContext : DbContext
    {
        public PersonalSiteDbContext(DbContextOptions<PersonalSiteDbContext> options) 
            : base(options)
        { }

        public DbSet<JobExperience> JobExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new JobExperienceMappingOverride());
        }
    }
}
