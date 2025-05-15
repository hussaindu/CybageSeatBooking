using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using CybageSeatBooking.Service.Interface;

namespace CybageSeatBooking.Service.Implemenatation
{
    public class SeatService : ISeatService
    {

        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task AddSeatAsync(SeatDto seatDto)
        {
            var seat = new Seat
            {
                SeatNumber = seatDto.SeatNumber
            };

            await _seatRepository.AddSeatAsync(seat);
        }

        public async Task<bool> DeleteSeatAsync(int id)
        {
            var seat = await _seatRepository.GetSeatByIdAsync(id);

            if (seat == null) { 
                return false;
            }

            await _seatRepository.DeleteSeatAsync(seat);
            return true;
        }

        public async Task<List<Seat>> GetAllSeatsAsync()
        {
            return await _seatRepository.GetAllSeatsAsync();
        }
    }
}
