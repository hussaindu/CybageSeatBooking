using CybageSeatBooking.Models;

namespace CybageSeatBooking.Repository.Interface
{
    public interface ISeatRepository
    {
        Task<List<Seat>> GetAllSeatsAsync();

        Task<Seat> GetSeatByIdAsync(int id);

        Task AddSeatAsync(Seat seat);

        Task DeleteSeatAsync(Seat seat);
            
    }
}
