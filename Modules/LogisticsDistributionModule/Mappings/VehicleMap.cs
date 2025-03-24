using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.LogisticsDistributionModule.Entities;

namespace Modules.LogisticsDistributionModule.Mappings;

public class VehicleMap : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.VIN)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.VehicleStatus)
            .IsRequired();

        builder.HasOne(v => v.ProductionOrder)
            .WithMany(p => p.Vehicles)
            .HasForeignKey(v => v.ProductionOrderId);

        builder.HasMany(v => v.Deliveries)
            .WithOne(d => d.Vehicle)
            .HasForeignKey(d => d.VehicleId);

        builder.ToTable("Vehicles");
    }
}