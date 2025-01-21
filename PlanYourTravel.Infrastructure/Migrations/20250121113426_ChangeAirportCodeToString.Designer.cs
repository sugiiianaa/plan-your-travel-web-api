﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PlanYourTravel.Infrastructure.Persistence;

#nullable disable

namespace PlanYourTravel.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250121113426_ChangeAirportCodeToString")]
    partial class ChangeAirportCodeToString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.AirlineAggregate.Airline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AirlineCode")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.AirportAggregate.Airport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FlightType")
                        .HasColumnType("integer");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.FlightScheduleAggregate.FlightSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AirlineId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ArrivalAirportId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ArrivalDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DepartureAirportId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DepartureDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("ArrivalAirportId");

                    b.HasIndex("DepartureAirportId");

                    b.ToTable("FlightSchedules");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.FlightScheduleAggregate.FlightSeatClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FlightScheduleId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("SeatClassType")
                        .HasColumnType("integer");

                    b.Property<int>("SeatsBooked")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FlightScheduleId");

                    b.ToTable("FlightSeatClasses");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.LocationAggregate.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.Transactions.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.Transactions.FlightTransaction", b =>
                {
                    b.HasBaseType("PlanYourTravel.Domain.Entities.Transactions.Transaction");

                    b.Property<Guid>("FlightSeatClassId")
                        .HasColumnType("uuid");

                    b.Property<int>("NumberOfSeatBooked")
                        .HasColumnType("integer");

                    b.Property<decimal>("SeatClassPriceAtBooking")
                        .HasColumnType("numeric");

                    b.HasIndex("FlightSeatClassId");

                    b.ToTable("FlightTransactions", (string)null);
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.AirportAggregate.Airport", b =>
                {
                    b.HasOne("PlanYourTravel.Domain.Entities.LocationAggregate.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.FlightScheduleAggregate.FlightSchedule", b =>
                {
                    b.HasOne("PlanYourTravel.Domain.Entities.AirlineAggregate.Airline", "Airline")
                        .WithMany()
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PlanYourTravel.Domain.Entities.AirportAggregate.Airport", "ArrivalAirport")
                        .WithMany()
                        .HasForeignKey("ArrivalAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PlanYourTravel.Domain.Entities.AirportAggregate.Airport", "DepartureAirport")
                        .WithMany()
                        .HasForeignKey("DepartureAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Airline");

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.FlightScheduleAggregate.FlightSeatClass", b =>
                {
                    b.HasOne("PlanYourTravel.Domain.Entities.FlightScheduleAggregate.FlightSchedule", null)
                        .WithMany("SeatClasses")
                        .HasForeignKey("FlightScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.Transactions.Transaction", b =>
                {
                    b.HasOne("PlanYourTravel.Domain.Entities.UserAggregate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.UserAggregate.User", b =>
                {
                    b.OwnsOne("PlanYourTravel.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("EmailAddress");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.Transactions.FlightTransaction", b =>
                {
                    b.HasOne("PlanYourTravel.Domain.Entities.FlightScheduleAggregate.FlightSeatClass", "FlightSeatClass")
                        .WithMany()
                        .HasForeignKey("FlightSeatClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanYourTravel.Domain.Entities.Transactions.Transaction", null)
                        .WithOne()
                        .HasForeignKey("PlanYourTravel.Domain.Entities.Transactions.FlightTransaction", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlightSeatClass");
                });

            modelBuilder.Entity("PlanYourTravel.Domain.Entities.FlightScheduleAggregate.FlightSchedule", b =>
                {
                    b.Navigation("SeatClasses");
                });
#pragma warning restore 612, 618
        }
    }
}
