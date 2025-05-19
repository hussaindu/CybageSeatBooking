using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using CybageSeatBooking.Service.Interface;
using Microsoft.EntityFrameworkCore;

public class SeatBookingService : ISeatBookingService
{
    private readonly ISeatBookingRepository _repository;

    public SeatBookingService(ISeatBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SeatBooking>> GetUserBookingsAsync(string userId)
    {
        return await _repository.GetBookingsByEmployeeIdAsync(userId);
    }

    public async Task<List<Seat>> GetAvailableSeatsAsync()
    {
        var allSeats = await _repository.GetAllSeatsWithBookingsAsync();

        var availableSeats = allSeats
            .Where(s => !s.seatBookings.Any(b =>
                b.StartDate <= DateTime.Now.AddDays(7) &&
                b.EndDate >= DateTime.Now))
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
}
