using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.LogisticsDistributionModule.Entities;
using Modules.LogisticsDistributionModule.Enums;

namespace Modules.LogisticsDistributionModule.Mappings;

public class DeliveryMap : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.DeliveryDate)
            .IsRequired();

        builder.Property(d => d.DeliveryStatus)
            .IsRequired();

        builder.HasOne(d => d.Vehicle)
            .WithMany(v => v.Deliveries)
            .HasForeignKey(d => d.VehicleId);

        builder.HasOne(d => d.Distributor)
            .WithMany()
            .HasForeignKey(d => d.DistributorId);

        builder.ToTable("Deliveries");
    }
}