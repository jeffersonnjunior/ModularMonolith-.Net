using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Production.Entities;

namespace Modules.Production.Mappings;

public class ProductionPartMapping : IEntityTypeConfiguration<ProductionPart>
{
    public void Configure(EntityTypeBuilder<ProductionPart> builder)
    {
        builder.ToTable("ProductionParts");

        builder.HasKey(pp => pp.Id);

        builder.Property(pp => pp.Id)
            .IsRequired();

        builder.Property(pp => pp.ProductionOrderId)
            .IsRequired();

        builder.Property(pp => pp.PartCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pp => pp.QuantityUsed)
            .IsRequired();

        builder.HasOne(pp => pp.ProductionOrder)
            .WithMany(po => po.Parts)
            .HasForeignKey(pp => pp.ProductionOrderId);
    }
}