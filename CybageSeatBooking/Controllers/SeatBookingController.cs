using CybageSeatBooking.Models;
using CybageSeatBooking.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Controllers
{
    public class SeatBookingController : Controller

    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SeatBookingController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var bookings = await _context.seatBookings
                .Include(b => b.Seat)
                .Include(b => b.User)
                .OrderByDescending(b => b.StartDate)
                .ToListAsync();
         

            return View(bookings);
        }

        public async Task<IActionResult> Create()
        {
            var allSeats = await _context.seats
                .Include(s => s.seatBookings)
                .ToListAsync();

            var availableSeats = allSeats
                .Where(s => !s.seatBookings.Any(b =>
                    b.StartDate <= DateTime.Now.AddDays(7) &&
                    b.EndDate >= DateTime.Now))
                .ToList();

            ViewBag.SeatList = allSeats;
            ViewBag.AvailableSeatIds = availableSeats.Select(s => s.Id).ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(SeatBooking booking)
        {
            booking.EmployeeId = _userManager.GetUserId(User);
            booking.StartDate = DateTime.Now;
            booking.EndDate = DateTime.Now.AddDays(7);

            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","SeatBooking");
        }


        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _context.seatBookings.FindAsync(id);

            if(booking == null)
            {
                return NotFound();
            }
            _context.seatBookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "SeatBooking");
        }
    }
    
        
}
