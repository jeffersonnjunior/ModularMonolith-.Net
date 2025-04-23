using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Sales.Entities;

namespace Modules.Sales.Mappings;

public class SaleDetailMapping : IEntityTypeConfiguration<SaleDetail>
{
    public void Configure(EntityTypeBuilder<SaleDetail> builder)
    {
        builder.ToTable("SaleDetails");

        builder.HasKey(sd => sd.Id);

        builder.Property(sd => sd.Id)
            .IsRequired();

        builder.Property(sd => sd.CarSaleId)
            .IsRequired();

        builder.Property(sd => sd.BuyerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sd => sd.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); 

        builder.Property(sd => sd.Discount)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); 

        builder.Property(sd => sd.PaymentMethod)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(sd => sd.Notes)
            .HasMaxLength(500);

        builder.HasOne(sd => sd.CarSale)
            .WithOne(cs => cs.SaleDetail)
            .HasForeignKey<SaleDetail>(sd => sd.CarSaleId);
    }
}