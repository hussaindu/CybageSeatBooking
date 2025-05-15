using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using CybageSeatBooking.Models;
using Microsoft.AspNetCore.Identity;

namespace CybageSeatBooking.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SeatBooking> seatBookings { get; set; }
        public DbSet<Seat> seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important to call this for Identity tables

            // Seat - SeatBooking relationship
            modelBuilder.Entity<Seat>()
                .HasMany(s => s.seatBookings)
                .WithOne(b => b.Seat)
                .HasForeignKey(b => b.SeatId)
                .OnDelete(DeleteBehavior.Cascade);

            // SeatBooking - ApplicationUser relationship
            modelBuilder.Entity<SeatBooking>()
                .HasOne(b => b.User)
                .WithMany() // If you add a navigation property in ApplicationUser, replace with .WithMany(u => u.SeatBookings)
                .HasForeignKey(b => b.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent user deletion from cascading to bookings
        }
    }
}
