using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using CybageSeatBooking.Service.Interface;

namespace CybageSeatBooking.Service.Implemenatation
{
    public class UserService: IUserService
    {
        private readonly IUsersRepository _userRepository;

        public UserService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return false;

            var result = await _userRepository.DeleteUserAsync(user);
            return result.Succeeded;
        }
    }
}
