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

    public async Task<bool> IsSeatAlreadyBookedAsync(int seatId, DateTime startDate, DateTime endDate)
    {
        return await _context.seatBookings
            .AnyAsync(b =>
                b.SeatId == seatId &&
                b.StartDate < endDate &&
                b.EndDate > startDate);
    }

    public async Task<bool> HasBookingForWeekAsync(string employeeId, DateTime startDate, DateTime endDate)
    {
        return await _context.seatBookings
            .AnyAsync(b =>
                b.EmployeeId == employeeId &&
                b.StartDate == startDate &&
                b.EndDate == endDate);
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

   
}
