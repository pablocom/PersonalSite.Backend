using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Persistence.Events;

namespace PersonalSite.Persistence.Mappings;

public class IntegrationEventMappingOverride : IEntityTypeConfiguration<IntegrationEvent>
{
    public void Configure(EntityTypeBuilder<IntegrationEvent> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.CreatedAt).HasPrecision(5).IsRequired();
        builder.Property(o => o.SerializedData).IsRequired();
        builder.Property(o => o.FullyQualifiedTypeName).IsRequired();

        builder.HasIndex(x => x.IsPublished);
    }
}
