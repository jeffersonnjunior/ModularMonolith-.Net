using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Production.Entities;

namespace Modules.Production.Mappings;

public class ProductionOrderMapping : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder.ToTable("ProductionOrders");

        builder.HasKey(po => po.Id);

        builder.Property(po => po.Id)
            .IsRequired();

        builder.Property(po => po.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.ProductionStatus)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(po => po.CreatedAt)
            .IsRequired();

        builder.Property(po => po.CompletedAt)
            .IsRequired(false);

        builder.HasMany(po => po.Parts)
            .WithOne(pp => pp.ProductionOrder)
            .HasForeignKey(pp => pp.ProductionOrderId);
    }
}