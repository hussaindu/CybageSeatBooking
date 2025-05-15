using CybageSeatBooking.Models;
using CybageSeatBooking.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CybageSeatBooking.Repository.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public bool IsUserSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }
    }
}
