using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.QualityInspectionModule.Entities;

namespace Modules.QualityInspectionModule.Mappings;

public class InspectionMap : IEntityTypeConfiguration<Inspection>
{
    public void Configure(EntityTypeBuilder<Inspection> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ProductionStageId)
            .IsRequired();

        builder.Property(i => i.InspectionDate)
            .IsRequired();

        builder.Property(i => i.InspectionResult)
            .IsRequired();

        builder.Property(i => i.Notes)
            .HasMaxLength(1000);

        builder.HasOne(i => i.ProductionStage)
            .WithMany(ps => ps.Inspections)
            .HasForeignKey(i => i.ProductionStageId);

        builder.HasMany(i => i.InspectionFailures)
            .WithOne(ins => ins.Inspection)
            .HasForeignKey(ins => ins.InspectionId);

        builder.ToTable("Inspections");
    }
}