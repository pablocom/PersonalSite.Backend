using System.Threading;
using System.Threading.Tasks;
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

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
