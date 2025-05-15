using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Repository.Implementation
{
    public class UserRepository:IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }
    }
}
