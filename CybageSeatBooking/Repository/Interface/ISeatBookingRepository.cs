using CybageSeatBooking.Models;

namespace CybageSeatBooking.Repository.Interface
{
    public interface ISeatBookingRepository
    {
        Task<List<SeatBooking>> GetBookingsByEmployeeIdAsync(string employeeId);
        Task<List<Seat>> GetAllSeatsWithBookingsAsync();
        Task AddBookingAsync(SeatBooking booking);
        Task<SeatBooking?> GetBookingByIdAsync(int id);
        Task DeleteBookingAsync(SeatBooking booking);
        Task SaveChangesAsync();
        Task<bool> IsSeatAlreadyBookedAsync(int seatId, DateTime startDate, DateTime endDate);

        

        Task<bool> HasBookingForWeekAsync(string employeeId, DateTime startDate, DateTime endDate);








    }
}
