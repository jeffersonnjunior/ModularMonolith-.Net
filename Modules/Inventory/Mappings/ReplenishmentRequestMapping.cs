using global::Modules.Inventory.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Inventory.Mappings;

public class ReplenishmentRequestMapping : IEntityTypeConfiguration<ReplenishmentRequest>
{
    public void Configure(EntityTypeBuilder<ReplenishmentRequest> builder)
    {
        builder.ToTable("ReplenishmentRequests");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired();

        builder.Property(r => r.PartCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.RequestedQuantity)
            .IsRequired();

        builder.Property(r => r.ReplenishmentStatus)
            .IsRequired()
            .HasConversion<int>(); 

        builder.Property(r => r.RequestedAt)
            .IsRequired();
    }
}