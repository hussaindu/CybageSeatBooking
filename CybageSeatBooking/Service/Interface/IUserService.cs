using CybageSeatBooking.Models;

namespace CybageSeatBooking.Service.Interface
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string id);
    }
}
