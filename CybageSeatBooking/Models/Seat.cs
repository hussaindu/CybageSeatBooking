using System.ComponentModel.DataAnnotations;

namespace CybageSeatBooking.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        public string SeatNumber { get; set; } = "";

        public ICollection<SeatBooking> seatBookings { get; set; }
    }
}
