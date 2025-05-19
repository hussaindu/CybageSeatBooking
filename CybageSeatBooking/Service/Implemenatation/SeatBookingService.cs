using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using CybageSeatBooking.Service.Interface;
using Microsoft.EntityFrameworkCore;

public class SeatBookingService : ISeatBookingService
{
    private readonly ISeatBookingRepository _repository;
    private readonly ApplicationDbContext _context;

    public SeatBookingService(ISeatBookingRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<List<SeatBooking>> GetUserBookingsAsync(string userId)
    {
        return await _repository.GetBookingsByEmployeeIdAsync(userId);
    }

    public async Task<List<Seat>> GetAvailableSeatsAsync()
    {
        var allSeats = await _repository.GetAllSeatsWithBookingsAsync();

        // Show seats not booked in the next 7 days (customize if needed)
        var now = DateTime.Now;
        var weekAhead = now.AddDays(7);

        var availableSeats = allSeats
            .Where(s => !s.seatBookings.Any(b =>
                b.StartDate < weekAhead &&
                b.EndDate > now))
            .ToList();

        return availableSeats;
    }

    public async Task CreateBookingAsync(SeatBooking booking)
    {
        await _repository.AddBookingAsync(booking);
    }

    public async Task<bool> CancelBookingAsync(int id)
    {
        var booking = await _repository.GetBookingByIdAsync(id);
        if (booking == null)
            return false;

        await _repository.DeleteBookingAsync(booking);
        return true;
    }

    public async Task<bool> IsSeatBookedForWeekAsync(int seatId, DateTime startDate, DateTime endDate)
    {
        return await _repository.IsSeatAlreadyBookedAsync(seatId, startDate, endDate);
    }

    public async Task<bool> HasBookingForWeekAsync(string employeeId, DateTime startDate, DateTime endDate)
    {
        return await _repository.HasBookingForWeekAsync(employeeId, startDate, endDate);
    }

    
}
