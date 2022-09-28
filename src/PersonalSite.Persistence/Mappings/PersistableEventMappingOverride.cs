using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalSite.Persistence.Events;

namespace PersonalSite.Persistence.Mappings;

public class PersistableEventMappingOverride : IEntityTypeConfiguration<PersistableEvent>
{
    public void Configure(EntityTypeBuilder<PersistableEvent> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.CreatedAt).HasPrecision(5).IsRequired();
        builder.Property(o => o.SerializedData).IsRequired();
        builder.Property(o => o.FullyQualifiedTypeName).IsRequired();

        builder.HasIndex(x => x.CreatedAt);
    }
}
