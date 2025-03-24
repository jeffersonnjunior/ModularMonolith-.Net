using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.ProductionInventoryModule.Entities;

namespace Modules.ProductionInventoryModule.Mappings;

public class MaterialMap : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .HasMaxLength(500);

        builder.Property(m => m.StockQuantity)
            .IsRequired();

        builder.Property(m => m.MinimumStock)
            .IsRequired();

        builder.Property(m => m.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasMany(m => m.OrderMaterials)
            .WithOne(om => om.Material)
            .HasForeignKey(om => om.MaterialId);

        builder.ToTable("Materials");
    }
}