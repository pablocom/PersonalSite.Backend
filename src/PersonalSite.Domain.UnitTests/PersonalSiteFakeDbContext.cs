using Microsoft.EntityFrameworkCore;
using PersonalSite.Persistence.Mappings;

namespace PersonalSite.Domain.UnitTests
{
    public class PersonalSiteFakeDbContext : DbContext
    {
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