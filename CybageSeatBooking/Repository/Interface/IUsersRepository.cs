using CybageSeatBooking.Models;
using Microsoft.AspNetCore.Identity;

namespace CybageSeatBooking.Repository.Interface
{
    public interface IUsersRepository
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
    }
}
