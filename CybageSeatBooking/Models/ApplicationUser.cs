using Microsoft.AspNetCore.Identity;

namespace CybageSeatBooking.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";
    
        public DateTime CreateAt { get; set; }
    }
}
