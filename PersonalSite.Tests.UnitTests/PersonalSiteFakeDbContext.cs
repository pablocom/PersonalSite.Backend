using Microsoft.EntityFrameworkCore;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence.Mappings;

namespace PersonalSite.Tests.UnitTests
{
    public class PersonalSiteFakeDbContext : DbContext
    {
        public DbSet<JobExperience> JobExperiences { get; set; }    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "FakePersonalSiteDbContext");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddPersonalSiteMappings(modelBuilder);
        }

        private void AddPersonalSiteMappings(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JobExperienceMappingOverride());
        }
    }
}