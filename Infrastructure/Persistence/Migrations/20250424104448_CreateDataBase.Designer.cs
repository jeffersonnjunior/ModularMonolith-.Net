﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250424104448_CreateDataBase")]
    partial class CreateDataBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Modules.Inventory.Entities.ReplenishmentRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PartCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ReplenishmentStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RequestedQuantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ReplenishmentRequests", (string)null);
                });

            modelBuilder.Entity("Modules.Inventory.Part", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("MinimumRequired")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Parts", (string)null);
                });

            modelBuilder.Entity("Modules.Production.Entities.ProductionOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ProductionStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ProductionOrders", (string)null);
                });

            modelBuilder.Entity("Modules.Production.Entities.ProductionPart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PartCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("ProductionOrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("QuantityUsed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductionOrderId");

                    b.ToTable("ProductionParts", (string)null);
                });

            modelBuilder.Entity("Modules.Sales.Entities.CarSale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductionOrderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("SoldAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CarSales", (string)null);
                });

            modelBuilder.Entity("Modules.Sales.Entities.SaleDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BuyerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("CarSaleId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CarSaleId")
                        .IsUnique();

                    b.ToTable("SaleDetails", (string)null);
                });

            modelBuilder.Entity("Modules.Production.Entities.ProductionPart", b =>
                {
                    b.HasOne("Modules.Production.Entities.ProductionOrder", "ProductionOrder")
                        .WithMany("Parts")
                        .HasForeignKey("ProductionOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionOrder");
                });

            modelBuilder.Entity("Modules.Sales.Entities.SaleDetail", b =>
                {
                    b.HasOne("Modules.Sales.Entities.CarSale", "CarSale")
                        .WithOne("SaleDetail")
                        .HasForeignKey("Modules.Sales.Entities.SaleDetail", "CarSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarSale");
                });

            modelBuilder.Entity("Modules.Production.Entities.ProductionOrder", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("Modules.Sales.Entities.CarSale", b =>
                {
                    b.Navigation("SaleDetail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
