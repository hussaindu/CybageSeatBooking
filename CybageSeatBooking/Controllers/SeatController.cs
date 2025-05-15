using CybageSeatBooking.Models;
using CybageSeatBooking.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CybageSeatBooking.Controllers
{
    public class SeatController : Controller
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService)
        {
            _seatService=seatService;
        }

        public async Task<IActionResult> Index()
        {
            
               var seat = await _seatService.GetAllSeatsAsync();
            
                return View(seat);
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
            if(!ModelState.IsValid)
            {
                return View(seatDto);
            }

            await _seatService.AddSeatAsync(seatDto);
               
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _seatService.DeleteSeatAsync(id);
            if (!success)
            {
                return NotFound();
            }
               

            return RedirectToAction(nameof(Index));
        }
    }
}
