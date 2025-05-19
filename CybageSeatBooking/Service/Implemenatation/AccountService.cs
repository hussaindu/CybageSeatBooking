using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using CybageSeatBooking.Service.Interface;

namespace CybageSeatBooking.Service.Implemenatation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var result = await accountRepository.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await accountRepository.SignOutAsync();
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                CreateAt = DateTime.Now
            };

            var result = await accountRepository.RegisterAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await accountRepository.AddToRoleAsync(user, "employee");
                await accountRepository.SignInAsync(user, false);
                return (true, Enumerable.Empty<string>());
            }

            return (false, result.Errors.Select(e => e.Description));
        }

    }
}
