﻿using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Entities;
using PlanYourTravel.Domain.Primitives;
using PlanYourTravel.Domain.Repositories;

namespace PlanYourTravel.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Airline> Airlines { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;
        public DbSet<FlightSchedule> FlightSchedules { get; set; } = null!;
        public DbSet<FlightSeatClass> FlightSeatClasses { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<FlightTransaction> FlightTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TPT: base and derived transaction tables
            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions");
            modelBuilder.Entity<FlightTransaction>()
                .ToTable("FlightTransactions");

            // FlightSchedule -> FlightSeatClass
            modelBuilder.Entity<FlightSeatClass>()
                .HasOne<FlightSchedule>()
                .WithMany(x => x.SeatClasses)
                .HasForeignKey(x => x.FlightScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            // FlightSchedule -> Airport (Departure)
            modelBuilder.Entity<FlightSchedule>()
                .HasOne(fs => fs.DepartureAirport)
                .WithMany()
                .HasForeignKey(fs => fs.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlightSchedule -> Airport (Arrival)
            modelBuilder.Entity<FlightSchedule>()
                .HasOne(fs => fs.ArrivalAirport)
                .WithMany()
                .HasForeignKey(fs => fs.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            // FlightSchedule -> Airline
            modelBuilder.Entity<FlightSchedule>()
                .HasOne(fs => fs.Airline)
                .WithMany()
                .HasForeignKey(fs => fs.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
