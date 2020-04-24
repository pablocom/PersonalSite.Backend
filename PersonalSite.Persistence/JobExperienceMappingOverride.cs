using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Domain;

namespace PersonalSite.Persistence
{
    class JobExperienceMappingOverride : IEntityTypeConfiguration<JobExperience>
    {
        public void Configure(EntityTypeBuilder<JobExperience> builder)
        {
            builder.HasKey(o => o.Id);
            builder.OwnsOne(o => o.JobPeriod);
        }
    }
}
