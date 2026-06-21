using Demo_Project.Models;
using Demo_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class BookingController : BaseController
    {
        private readonly HotelDbContext _context;

        public BookingController(HotelDbContext context)
        {
            _context = context;
        }

        // Search Page

        [HttpGet]
        public IActionResult SearchRooms()
        {
            ViewBag.CheckIn = DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.CheckOut = "";
            return View(new List<Room>());
        }

        // Available Rooms

        [HttpPost]
        public IActionResult SearchRooms(DateTime checkIn, DateTime checkOut)
        {
            // Past date validation
            if (checkIn.Date < DateTime.Today)
            {
                TempData["Error"] = "Check-In date cannot be in the past.";
                return RedirectToAction("SearchRooms");
            }

            // Check-Out must be after Check-In
            if (checkOut.Date <= checkIn.Date)
            {
                TempData["Error"] = "Check-Out date must be greater than Check-In date.";
                return RedirectToAction("SearchRooms");
            }

            var rooms = _context.Rooms
                .Include(x => x.RoomType)
                .Include(x => x.RoomImages)
                .Where(x => x.IsActive)
                  .Where(x => x.Status != RoomStatus.Maintenance)
                .Where(x => !_context.Bookings.Any(b =>
                        b.RoomId == x.Id
                        && b.Status != BookingStatus.Cancelled
                         && b.Status != BookingStatus.CheckedOut
                        && checkIn < b.CheckOutDate
                        && checkOut > b.CheckInDate))
                .ToList();

            ViewBag.CheckIn = checkIn.ToString("yyyy-MM-dd");
            ViewBag.CheckOut = checkOut.ToString("yyyy-MM-dd");

            return View(rooms);
        }

        // Booking Page

        [HttpGet]
        public IActionResult BookRoom(long roomId,
            DateTime checkInDate,
            DateTime checkOutDate)
        {

            var room = _context.Rooms
                .Include(x => x.RoomType)
                .FirstOrDefault(x => x.Id == roomId);

            if (room == null)
                return NotFound();

            if (room.Status == RoomStatus.Maintenance)
            {
                TempData["Error"] = "This room is under maintenance.";
                return RedirectToAction("SearchRooms");
            }

            int nights = (checkOutDate - checkInDate).Days;

            BookingVM vm = new BookingVM
            {
                RoomId = room.Id,

                RoomNumber = room.RoomNumber,

                RoomType = room.RoomType?.RoomTypeName ?? "",

                PricePerNight = room.PricePerNight,

                CheckInDate = checkInDate,

                CheckOutDate = checkOutDate,

                TotalNights = (checkOutDate - checkInDate).Days,
                TotalAmount = room.PricePerNight * (checkOutDate - checkInDate).Days
            };

            return View(vm);
        }

        // Save Booking

        [HttpPost]
        public IActionResult ConfirmBooking(BookingVM model)
        {
            if (model.DateOfBirth.HasValue)
            {
                var age = DateTime.Today.Year - model.DateOfBirth.Value.Year;

                if (model.DateOfBirth.Value.Date > DateTime.Today.AddYears(-age))
                {
                    age--;
                }

                if (age < 18)
                {
                    ModelState.AddModelError("DateOfBirth",
                        "Guest must be at least 18 years old.");
                    TempData["Error"] = "Guest must be at least 18 years old.";
                }
            }
            if (!ModelState.IsValid)
                return View("BookRoom", model);
            string imagePath = "";

            if (model.IdentityImage != null)
            {
                string folder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/customerids");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(model.IdentityImage.FileName);

                string fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    model.IdentityImage.CopyTo(stream);
                }

                imagePath = "/uploads/customerids/" + fileName;
            }
            Customer customer = new Customer
            {
                FullName = model.FullName,
                Mobile = model.Mobile,
                Email = model.Email,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Country = model.Country,
                Pincode = model.Pincode,
                IdentityType = model.IdentityType,
                IdentityNumber = model.IdentityNumber,
                IdentityImagePath = imagePath,
                EmergencyContactName = model.EmergencyContactName,
                EmergencyContactMobile = model.EmergencyContactMobile,


                CreatedOn = DateTime.Now

            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            Booking booking = new Booking
            {

                BookingNumber = "BK" +
                                DateTime.Now.ToString("yyyyMMddHHmmss"),

                CustomerId = customer.Id,

                RoomId = model.RoomId,

                CheckInDate = model.CheckInDate,

                CheckOutDate = model.CheckOutDate,

                TotalNights = model.TotalNights,

                Adults = model.Adults,

                Children = model.Children,

                PricePerNight = model.PricePerNight,

                TotalAmount = model.TotalAmount,

                Status = BookingStatus.Booked,

                CreatedByUserId = Convert.ToInt64(
    HttpContext.Session.GetString("UserId")
),

                CreatedOn = DateTime.Now
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Invoice",
                new { id = booking.Id });
        }

        // Invoice
        //Get
        public IActionResult Invoice(long id)
        {
            var booking = _context.Bookings
       .Include(x => x.Customer)
       .Include(x => x.Room)

       .ThenInclude(x => x.RoomType)
       .FirstOrDefault(x => x.Id == id);

            return View(booking);
        }
        //Get
        public IActionResult InvoiceList()
        {
            var bookings = _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Room)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(bookings);
        }

        //Post
    }
}
