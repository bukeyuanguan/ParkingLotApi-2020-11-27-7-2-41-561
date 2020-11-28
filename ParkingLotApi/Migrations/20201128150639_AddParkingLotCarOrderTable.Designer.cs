﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Migrations
{
    [DbContext(typeof(ParkingLotDbContext))]
    [Migration("20201128150639_AddParkingLotCarOrderTable")]
    partial class AddParkingLotCarOrderTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ParkingLotApi.Entities.CarEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ParkingLotEntityId")
                        .HasColumnType("int");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ParkingLotEntityId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("ParkingLotApi.Entities.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CloseTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreationTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("OrderStatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ParkingLotEntityId")
                        .HasColumnType("int");

                    b.Property<string>("ParkingLotName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ParkingLotEntityId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ParkingLotApi.Entities.ParkingLotEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Locatoin")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ParkingLots");
                });

            modelBuilder.Entity("ParkingLotApi.Entities.CarEntity", b =>
                {
                    b.HasOne("ParkingLotApi.Entities.ParkingLotEntity", null)
                        .WithMany("Cars")
                        .HasForeignKey("ParkingLotEntityId");
                });

            modelBuilder.Entity("ParkingLotApi.Entities.OrderEntity", b =>
                {
                    b.HasOne("ParkingLotApi.Entities.ParkingLotEntity", null)
                        .WithMany("Orders")
                        .HasForeignKey("ParkingLotEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
