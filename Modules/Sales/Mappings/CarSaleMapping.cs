using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Sales.Entities;

namespace Modules.Sales.Mappings;

public class CarSaleMapping : IEntityTypeConfiguration<CarSale>
{
    public void Configure(EntityTypeBuilder<CarSale> builder)
    {
        builder.ToTable("CarSales");

        builder.HasKey(cs => cs.Id);

        builder.Property(cs => cs.Id)
            .IsRequired();

        builder.Property(cs => cs.ProductionOrderId)
            .IsRequired();

        builder.Property(cs => cs.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(cs => cs.SoldAt)
            .IsRequired(false);

        builder.HasOne(cs => cs.SaleDetail)
            .WithOne(sd => sd.CarSale)
            .HasForeignKey<SaleDetail>(sd => sd.CarSaleId);
    }
}