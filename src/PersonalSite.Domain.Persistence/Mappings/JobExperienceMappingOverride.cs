using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.WebApi.Model.JobExperienceAggregate;

namespace PersonalSite.Persistence.Mappings;

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
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            )
            .Metadata
            .SetValueComparer(new ValueComparer<ICollection<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c)
            );
    }
}
