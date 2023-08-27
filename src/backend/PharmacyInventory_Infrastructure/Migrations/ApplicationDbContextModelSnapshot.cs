﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PharmacyInventory_Infrastructure.Persistence;

#nullable disable

namespace PharmacyInventory_Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Brand", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("brand");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Drug", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrandId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrugCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GenericNameId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("money");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<string>("SupplierId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UnitId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("GenericNameId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UnitId");

                    b.HasIndex("UserId");

                    b.ToTable("drug");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.GenericName", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("genericName");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Supplier", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("supplier");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Unit", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("ConversionFactor")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("unit");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Drug", b =>
                {
                    b.HasOne("PharmacyInventory_Domain.Entities.Brand", "Brand")
                        .WithMany("Drugs")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PharmacyInventory_Domain.Entities.GenericName", "GenericName")
                        .WithMany("Drugs")
                        .HasForeignKey("GenericNameId");

                    b.HasOne("PharmacyInventory_Domain.Entities.Supplier", "Supplier")
                        .WithMany("Drugs")
                        .HasForeignKey("SupplierId");

                    b.HasOne("PharmacyInventory_Domain.Entities.Unit", "Unit")
                        .WithMany("Drugs")
                        .HasForeignKey("UnitId");

                    b.HasOne("PharmacyInventory_Domain.Entities.User", null)
                        .WithMany("Drugs")
                        .HasForeignKey("UserId");

                    b.Navigation("Brand");

                    b.Navigation("GenericName");

                    b.Navigation("Supplier");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Brand", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.GenericName", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.Unit", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("PharmacyInventory_Domain.Entities.User", b =>
                {
                    b.Navigation("Drugs");
                });
#pragma warning restore 612, 618
        }
    }
}
