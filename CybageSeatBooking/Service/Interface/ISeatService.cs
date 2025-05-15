using CybageSeatBooking.Models;

namespace CybageSeatBooking.Service.Interface
{
    public interface ISeatService
    {
        Task<List<Seat>> GetAllSeatsAsync();

        Task AddSeatAsync(SeatDto seatDto);

        Task<bool> DeleteSeatAsync(int id);
    }
}
