using CybageSeatBooking.Models;

namespace CybageSeatBooking.Service.Interface
{
    public interface ISeatBookingService
    {
        Task<List<SeatBooking>> GetUserBookingsAsync(string userId);
        Task<List<Seat>> GetAvailableSeatsAsync();
        Task CreateBookingAsync(SeatBooking booking);
        Task<bool> CancelBookingAsync(int id);

       

        

        Task<bool> IsSeatBookedForWeekAsync(int seatId, DateTime startDate, DateTime endDate);
        public  Task<bool> HasBookingForWeekAsync(string employeeId, DateTime startDate, DateTime endDate);

    }
}
