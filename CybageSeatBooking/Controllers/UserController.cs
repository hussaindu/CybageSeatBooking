using CybageSeatBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager=userManager;
            _context=context;
        }
        public IActionResult Index()
        {
            var allUsers=_userManager.Users.OrderByDescending(x => x.CreateAt).ToList();
            return View(allUsers);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var userId = await _userManager.FindByIdAsync(id);
            if (userId == null)
            {
                return NotFound();
            }

            var result =await _userManager.DeleteAsync(userId);
             
           
            return RedirectToAction("Index","User");

        }
    }
}
