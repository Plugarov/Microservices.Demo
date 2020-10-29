﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkshopManagementEventHandler.DataAccess;

namespace WorkshopManagementEventHandler.Migrations
{
    [DbContext(typeof(WorkshopManagementDBContext))]
    [Migration("20201028181417_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorkshopManagementEventHandler.Model.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("WorkshopManagementEventHandler.Model.MaintenanceJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActualEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ActualStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("VehicleLicenseNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("WorkshopPlanningDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VehicleLicenseNumber");

                    b.ToTable("MaintenanceJob");
                });

            modelBuilder.Entity("WorkshopManagementEventHandler.Model.Vehicle", b =>
                {
                    b.Property<string>("LicenseNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LicenseNumber");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("WorkshopManagementEventHandler.Model.MaintenanceJob", b =>
                {
                    b.HasOne("WorkshopManagementEventHandler.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("WorkshopManagementEventHandler.Model.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleLicenseNumber");
                });
#pragma warning restore 612, 618
        }
    }
}