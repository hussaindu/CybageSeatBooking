using CybageSeatBooking.Models;
using CybageSeatBooking.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Controllers
{
    public class UserController : Controller
    {
       
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
          
            _userService=userService;
        }
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userService.GetAllUsersAsync();
            return View(allUsers);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction("Index", "User");
        }
    }
}
