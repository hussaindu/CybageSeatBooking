using CybageSeatBooking.Models;

namespace CybageSeatBooking.Service.Interface
{
    public interface IAccountService
    {
        Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterDto registerDto);
        Task<bool> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
