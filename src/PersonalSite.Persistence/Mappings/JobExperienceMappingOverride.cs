using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Persistence.Mappings
{
    public class JobExperienceMappingOverride : IEntityTypeConfiguration<JobExperience>
    {
        public void Configure(EntityTypeBuilder<JobExperience> builder)
        {
            builder.HasKey(o => o.Id);
            builder.OwnsOne(o => o.JobPeriod,
                x =>
                {
                    x.Property(j => j.Start);
                    x.Property(j => j.End);
                });

            builder.Property(x => x.TechStack)
                .HasConversion(v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
