using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.QualityInspectionModule.Entities;

namespace Modules.QualityInspectionModule.Mappings;

public class InspectionFailureMap : IEntityTypeConfiguration<InspectionFailure>
{
    public void Configure(EntityTypeBuilder<InspectionFailure> builder)
    {
        builder.HasKey(inf => inf.Id);

        builder.Property(inf => inf.InspectionId)
            .IsRequired();

        builder.Property(inf => inf.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(inf => inf.CorrectionActionType)
            .IsRequired();

        builder.HasOne(inf => inf.Inspection)
            .WithMany(i => i.InspectionFailures)
            .HasForeignKey(inf => inf.InspectionId);

        builder.ToTable("InspectionFailures");
    }
}