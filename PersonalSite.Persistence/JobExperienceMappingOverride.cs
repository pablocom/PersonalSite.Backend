using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Domain;
using System;

namespace PersonalSite.Persistence
{
    class JobExperienceMappingOverride : IEntityTypeConfiguration<JobExperience>
    {
        public void Configure(EntityTypeBuilder<JobExperience> builder)
        {
            builder.HasKey(o => o.Id);
            builder.OwnsOne(o => o.JobPeriod);

            
            builder.Property(x => x.TechStack)
                .HasConversion(v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
