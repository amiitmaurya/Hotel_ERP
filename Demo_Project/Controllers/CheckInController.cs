using Demo_Project.Models;
using Demo_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class CheckInController : BaseController
    {
        private readonly HotelDbContext _context;

        public CheckInController(HotelDbContext context)
        {
            _context = context;
        }

        // LIST
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

        // CHECK IN PAGE
        [HttpGet]
        public IActionResult Create(long bookingId)
        {
            var booking = _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Room)
                .FirstOrDefault(x => x.Id == bookingId);

            if (booking == null)
                return NotFound();

            if (_context.CheckIns.Any(x => x.BookingId == bookingId))
            {
                TempData["Error"] = "Guest already checked in.";
                return RedirectToAction("Index", "CheckIn");
            }

            return View(booking);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CheckIn model)
        {
            var booking = _context.Bookings
                .FirstOrDefault(x => x.Id == model.BookingId);

            if (booking == null)
                return NotFound();

            long userId = Convert.ToInt64(
                HttpContext.Session.GetString("UserId"));

            CheckIn checkIn = new CheckIn
            {
                BookingId = model.BookingId,
                CheckInDateTime = DateTime.Now,
                CheckedInByUserId = userId
            };

            _context.CheckIns.Add(checkIn);

            // IMPORTANT
            booking.Status = BookingStatus.CheckedIn;

            _context.Entry(booking).State = EntityState.Modified;

            _context.SaveChanges();

            TempData["Success"] = "Guest checked in successfully.";

            return RedirectToAction("Index", "CheckIn");
        }
    }
}
