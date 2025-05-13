using CybageSeatBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Controllers
{
    public class SeatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.seats
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeatDto seatDto)
        {
            if (!ModelState.IsValid)
            {
                return View(seatDto);
            }

            var seat = new Seat
            {
                SeatNumber = seatDto.SeatNumber
            };

            _context.Add(seat);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var seat = await _context.seats.FindAsync(id);

            if (seat == null) 
            {
                return NotFound();
            }

            _context.Remove(seat);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }
    }
}
