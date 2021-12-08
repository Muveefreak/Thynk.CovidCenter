﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thynk.CovidCenter.Repository;

namespace Thynk.CovidCenter.Repository.Migrations
{
    [DbContext(typeof(CovidCenterDbContext))]
    [Migration("20211205151243_update-db")]
    partial class updatedb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.AvailableDate", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<long>("AvailableSlots")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateAvailable")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("DateAvailable");

                    b.HasIndex("LocationId");

                    b.ToTable("AvailableDates");
                });

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.Booking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AvailableDateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AvailableDateSelected")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingResult")
                        .HasColumnType("int");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCancelled")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("IndividualName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LocationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TestType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("AvailableDateId");

                    b.HasIndex("LocationID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.Location", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.AvailableDate", b =>
                {
                    b.HasOne("Thynk.CovidCenter.Data.Models.Location", "Location")
                        .WithMany("AvailableDates")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.Booking", b =>
                {
                    b.HasOne("Thynk.CovidCenter.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thynk.CovidCenter.Data.Models.AvailableDate", "AvailableDate")
                        .WithMany("Bookings")
                        .HasForeignKey("AvailableDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thynk.CovidCenter.Data.Models.Location", "Location")
                        .WithMany("Bookings")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("AvailableDate");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.AvailableDate", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Thynk.CovidCenter.Data.Models.Location", b =>
                {
                    b.Navigation("AvailableDates");

                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
