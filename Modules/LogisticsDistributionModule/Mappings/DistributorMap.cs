using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.LogisticsDistributionModule.Entities;

namespace Modules.LogisticsDistributionModule.Mappings;

public class DistributorMap : IEntityTypeConfiguration<Distributor>
{
    public void Configure(EntityTypeBuilder<Distributor> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Phone)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(d => d.Deliveries)
            .WithOne(d => d.Distributor)
            .HasForeignKey(d => d.DistributorId);

        builder.ToTable("Distributors");
    }
}