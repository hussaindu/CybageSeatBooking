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

        public SeatBookingController(
            ISeatBookingService seatBookingService,
            UserManager<ApplicationUser> userManager)
        {
            _seatBookingService = seatBookingService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Error", "Home");

            var bookings = await _seatBookingService.GetUserBookingsAsync(userId);
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Error", "Home");

            var (nextWeekStart, nextWeekEnd) = GetNextWeekDateRange();

            
            var alreadyBooked = await _seatBookingService.HasBookingForWeekAsync(userId, nextWeekStart, nextWeekEnd);
            if (alreadyBooked)
            {
                TempData["Error"] = "You have already booked a seat for next week.";
                return RedirectToAction("Index");
            }

            var availableSeats = await _seatBookingService.GetAvailableSeatsAsync();
            ViewBag.SeatList = availableSeats;
            ViewBag.AvailableSeatIds = availableSeats.Select(s => s.Id).ToList();

            ViewBag.NextWeekStart = nextWeekStart.ToString("MMMM dd");
            ViewBag.NextWeekEnd = nextWeekEnd.ToString("MMMM dd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeatBooking booking)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("User");
            ModelState.Remove("Seat");

            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Error", "Home");

            var (nextWeekStart, nextWeekEnd) = GetNextWeekDateRange();

            var alreadyBooked = await _seatBookingService.HasBookingForWeekAsync(userId, nextWeekStart, nextWeekEnd);
            if (alreadyBooked)
            {
                TempData["Error"] = "You have already booked a seat for next week.";
                return RedirectToAction("Index");
            }

            var isSeatBooked = await _seatBookingService.IsSeatBookedForWeekAsync(booking.SeatId, nextWeekStart, nextWeekEnd);
            if (isSeatBooked)
            {
                ModelState.AddModelError("", "This seat is already booked for next week. Please choose another.");
                await PopulateSeatViewDataAsync();
                return View(booking);
            }

            booking.EmployeeId = userId;
            booking.StartDate = nextWeekStart;
            booking.EndDate = nextWeekEnd;

            await _seatBookingService.CreateBookingAsync(booking);
            TempData["Success"] = "Seat booked successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _seatBookingService.CancelBookingAsync(id);
            if (!result)
                return NotFound();

            TempData["Success"] = "Booking cancelled successfully!";
            return RedirectToAction("Index");
        }

        private (DateTime start, DateTime end) GetNextWeekDateRange()
        {
            var today = DateTime.Today;
            var nextSunday = today.AddDays(7 - (int)today.DayOfWeek);
            var followingSunday = nextSunday.AddDays(7);
            return (nextSunday, followingSunday);
        }

        private async Task PopulateSeatViewDataAsync()
        {
            var seats = await _seatBookingService.GetAvailableSeatsAsync();
            ViewBag.SeatList = seats;
            ViewBag.AvailableSeatIds = seats.Select(s => s.Id).ToList();

            var (start, end) = GetNextWeekDateRange();
            ViewBag.NextWeekStart = start.ToString("MMMM dd");
            ViewBag.NextWeekEnd = end.ToString("MMMM dd");
        }
    }
}
