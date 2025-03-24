using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.ProductionInventoryModule.Entities;

namespace Modules.ProductionInventoryModule.Mappings;

public class ProductionOrderMap : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder.HasKey(po => po.Id);

        builder.Property(po => po.StartDate)
            .IsRequired();

        builder.Property(po => po.EndDate);

        builder.Property(po => po.ProductionOrderStatus)
            .IsRequired();

        builder.Property(po => po.TotalCost);

        builder.Property(po => po.OrderMaterialId)
            .IsRequired();

        builder.HasOne(po => po.OrderMaterial)
            .WithMany(om => om.ProductionOrders)
            .HasForeignKey(po => po.OrderMaterialId);

        builder.HasMany(po => po.ProductionStages)
            .WithOne(ps => ps.ProductionOrder)
            .HasForeignKey(ps => ps.ProductionOrderId);

        builder.HasMany(po => po.Vehicles)
            .WithOne(v => v.ProductionOrder)
            .HasForeignKey(v => v.ProductionOrderId);

        builder.ToTable("ProductionOrders");
    }
}