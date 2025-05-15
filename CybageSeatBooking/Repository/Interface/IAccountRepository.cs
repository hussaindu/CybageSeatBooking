using CybageSeatBooking.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CybageSeatBooking.Repository.Interface
{
    public interface IAccountRepository
    {

        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);

        Task SignInAsync(ApplicationUser user, bool isPersistent);

        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);

        Task SignOutAsync();

        Task AddToRoleAsync(ApplicationUser user, string role);

        bool IsUserSignedIn(ClaimsPrincipal user);

    }
}
