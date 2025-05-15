using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Repository.Implementation
{
    public class SeatRepository : ISeatRepository
    {

        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task AddSeatAsync(Seat seat)
        {
            _context.Add(seat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSeatAsync(Seat seat)
        {
           _context.Remove(seat);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Seat>> GetAllSeatsAsync()
        {
            return await _context.seats
                .Include(s => s.seatBookings)
                .ThenInclude(b => b.User)
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }

        public async Task<Seat> GetSeatByIdAsync(int id)
        {
            return await _context.seats.FindAsync(id);
        }
    }
}
