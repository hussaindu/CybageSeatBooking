using CybageSeatBooking.Models;
using CybageSeatBooking.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CybageSeatBooking.Controllers
{
    public class SeatController : Controller
    {
        private readonly ISeatService _seatService;
        private const int PageSize = 5;  

        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var allSeats = await _seatService.GetAllSeatsAsync();

            var count = allSeats.Count;
            var totalPages = (int)Math.Ceiling(count / (double)PageSize);

            var seats = allSeats
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(seats);
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
