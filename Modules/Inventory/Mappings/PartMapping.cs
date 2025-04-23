using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Inventory;

namespace Modules.Inventory.Mappings;

public class PartMapping : IEntityTypeConfiguration<Part>
{
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.ToTable("Parts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .HasMaxLength(200);

        builder.Property(p => p.QuantityInStock)
            .IsRequired();

        builder.Property(p => p.MinimumRequired)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();
    }
}