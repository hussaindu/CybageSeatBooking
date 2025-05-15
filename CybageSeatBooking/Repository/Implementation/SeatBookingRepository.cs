using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

public class SeatBookingRepository : ISeatBookingRepository
{
    private readonly ApplicationDbContext _context;

    public SeatBookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SeatBooking>> GetBookingsByEmployeeIdAsync(string employeeId)
    {
        return await _context.seatBookings
            .Where(b => b.EmployeeId == employeeId)
            .Include(b => b.Seat)
            .Include(b => b.User)
            .OrderByDescending(b => b.StartDate)
            .ToListAsync();
    }

    public async Task<List<Seat>> GetAllSeatsWithBookingsAsync()
    {
        return await _context.seats
            .Include(s => s.seatBookings)
            .ToListAsync();
    }

    public async Task AddBookingAsync(SeatBooking booking)
    {
        await _context.seatBookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<SeatBooking?> GetBookingByIdAsync(int id)
    {
        return await _context.seatBookings.FindAsync(id);
    }

    public async Task DeleteBookingAsync(SeatBooking booking)
    {
        _context.seatBookings.Remove(booking);
        await _context.SaveChangesAsync();
    }

    public  async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
