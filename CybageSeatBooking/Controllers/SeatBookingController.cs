using CybageSeatBooking.Models;
using CybageSeatBooking.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CybageSeatBooking.Controllers
{
    [Authorize]
    public class SeatBookingController : Controller
    {
        private readonly ISeatBookingService _seatBookingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeatBookingController(ISeatBookingService seatBookingService, UserManager<ApplicationUser> userManager)
        {
            _seatBookingService = seatBookingService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                // Redirect to an error page or return an appropriate message
                return RedirectToAction("Error", "Home");
            }

            var bookings = await _seatBookingService.GetUserBookingsAsync(userId);
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var availableSeats = await _seatBookingService.GetAvailableSeatsAsync();

            ViewBag.SeatList = availableSeats;
            ViewBag.AvailableSeatIds = availableSeats.Select(s => s.Id).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeatBooking booking)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("User");
            ModelState.Remove("Seat");

            if (!ModelState.IsValid)
            {
                var availableSeats = await _seatBookingService.GetAvailableSeatsAsync();
                ViewBag.AvailableSeatIds = availableSeats.Select(s => s.Id).ToList();
                ViewBag.SeatList = availableSeats;

                return View(booking);
            }

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Error", "Home");
            }

            booking.EmployeeId = userId;
            booking.StartDate = DateTime.Now;
            booking.EndDate = DateTime.Now.AddDays(7);

            await _seatBookingService.CreateBookingAsync(booking);
            return RedirectToAction("Index", "SeatBooking");
        }


        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _seatBookingService.CancelBookingAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "SeatBooking");
        }
    }
}
