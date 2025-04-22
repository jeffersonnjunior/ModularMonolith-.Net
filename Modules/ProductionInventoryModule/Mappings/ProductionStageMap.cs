using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.ProductionInventoryModule.Entities;

namespace Modules.ProductionInventoryModule.Mappings;

public class ProductionStageMap : IEntityTypeConfiguration<ProductionStage>
{
    public void Configure(EntityTypeBuilder<ProductionStage> builder)
    {
        builder.HasKey(ps => ps.Id);

        builder.Property(ps => ps.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ps => ps.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(ps => ps.ProductionOrderId)
            .IsRequired();

        builder.HasOne(ps => ps.ProductionOrder)
            .WithMany(po => po.ProductionStages)
            .HasForeignKey(ps => ps.ProductionOrderId);

        builder.HasMany(ps => ps.Inspections)
            .WithOne(i => i.ProductionStage)
            .HasForeignKey(i => i.ProductionStageId);

        builder.ToTable("ProductionStages");
    }
}