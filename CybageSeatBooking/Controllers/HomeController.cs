using CybageSeatBooking.Models;
using CybageSeatBooking.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CybageSeatBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
