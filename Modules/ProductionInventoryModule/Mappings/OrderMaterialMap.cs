using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.ProductionInventoryModule.Entities;

namespace Modules.ProductionInventoryModule.Mappings;

public class OrderMaterialMap : IEntityTypeConfiguration<OrderMaterial>
{
    public void Configure(EntityTypeBuilder<OrderMaterial> builder)
    {
        builder.HasKey(om => om.Id);

        builder.Property(om => om.MaterialId)
            .IsRequired();

        builder.Property(om => om.QuantityUsed)
            .IsRequired();

        builder.HasOne(om => om.Material)
            .WithMany(m => m.OrderMaterials)
            .HasForeignKey(om => om.MaterialId);

        builder.HasMany(om => om.ProductionOrders)
            .WithOne(po => po.OrderMaterial)
            .HasForeignKey(po => po.OrderMaterialId);

        builder.ToTable("OrderMaterials");
    }
}