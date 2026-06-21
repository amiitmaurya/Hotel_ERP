using Demo_Project.Models;
using Demo_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class CheckOutController : BaseController
    {
        private readonly HotelDbContext _context;

        public CheckOutController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookings = _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Room)
                .Include(x => x.CheckIn)
                .Include(x => x.CheckOut)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();

            return View(bookings);
        }

        [HttpGet]
        public IActionResult Create(long bookingId)
        {
            var booking = _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Room)
                .FirstOrDefault(x => x.Id == bookingId);

            if (booking == null)
                return NotFound();

            if (booking.Status != BookingStatus.CheckedIn)
            {
                TempData["Error"] = "Guest is not checked in.";
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CheckOut model)
        {
            var booking = _context.Bookings
                .FirstOrDefault(x => x.Id == model.BookingId);

            if (booking == null)
                return NotFound();

            long userId = Convert.ToInt64(
                HttpContext.Session.GetString("UserId"));

            CheckOut checkOut = new CheckOut
            {
                BookingId = model.BookingId,
                CheckOutDateTime = DateTime.Now,
                ExtraCharges = model.ExtraCharges,
                Remarks = model.Remarks,
                CheckedOutByUserId = userId
            };

            _context.CheckOuts.Add(checkOut);

            booking.Status = BookingStatus.CheckedOut;

            _context.SaveChanges();

            TempData["Success"] = "Guest checked out successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
