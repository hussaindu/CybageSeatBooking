using System.ComponentModel.DataAnnotations;

namespace CybageSeatBooking.Models
{
    public class SeatBooking
    {
        public int Id {  get; set; }

        [Required]

        public string EmployeeId { get; set; } = "";

        public ApplicationUser User { get; set; }

        [Required]
        
        public int SeatId { get; set; } //foreign key

        public Seat Seat { get; set; } // navigation property

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
