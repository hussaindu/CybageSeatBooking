using CybageSeatBooking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Service
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SeatBooking> seatBookings { get; set; }

        public DbSet<Seat> seats {  get; set; } 
    }


}
