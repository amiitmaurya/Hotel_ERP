using Demo_Project.Models;
using Demo_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class ManageBookingController : BaseController
    {
        private readonly HotelDbContext _context;

        public ManageBookingController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult ManageBooking()
        {
            var bookings = _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Room)
                .OrderByDescending(x => x.Id)
                .Select(x => new ManageBookingVM
                {
                    Id = x.Id,

                    BookingNumber = x.BookingNumber,

                    CustomerName = x.Customer != null
                        ? x.Customer.FullName
                        : "",

                    Mobile = x.Customer != null
                        ? x.Customer.Mobile
                        : "",

                    RoomNumber = x.Room != null
                        ? x.Room.RoomNumber
                        : "",

                    CheckInDate = x.CheckInDate,

                    CheckOutDate = x.CheckOutDate,

                    Adults = x.Adults,

                    Children = x.Children,

                    TotalAmount = x.TotalAmount,

                    Status = x.Status
                })
                .ToList();

            return View(bookings);
        }

        public IActionResult CancelBooking(long id)
        {
            var booking = _context.Bookings
                .FirstOrDefault(x => x.Id == id);

            //if (booking != null)
            //{
            //    booking.Status = BookingStatus.Cancelled;
            //    booking.CancelledOn = DateTime.Now;

            //    _context.SaveChanges();
            //}

            if (booking == null)
                return NotFound();

            if (booking.Status == BookingStatus.CheckedIn ||
                booking.Status == BookingStatus.CheckedOut)
            {
                TempData["Error"] =
                    "Checked-In or Checked-Out booking cannot be cancelled.";

                return RedirectToAction(nameof(ManageBooking));
            }

            booking.Status = BookingStatus.Cancelled;
            booking.CancelledOn = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction(nameof(ManageBooking));
        }

        [HttpGet]
        public IActionResult EditBooking(long id)
        {
            var booking = _context.Bookings
                .Include(x => x.Customer)
                .Include(x => x.Room)
                    .ThenInclude(x => x.RoomType)
                .FirstOrDefault(x => x.Id == id);

            if (booking == null)
                return NotFound();

            BookingVM vm = new BookingVM
            {
                RoomId = booking.RoomId,

                RoomNumber = booking.Room?.RoomNumber ?? "",

                RoomType = booking.Room?.RoomType?.RoomTypeName ?? "",

                PricePerNight = booking.PricePerNight,

                FullName = booking.Customer?.FullName ?? "",

                Mobile = booking.Customer?.Mobile ?? "",

                Email = booking.Customer?.Email ?? "",

                Gender = booking.Customer?.Gender ?? "",

                DateOfBirth = booking.Customer?.DateOfBirth,

                Address = booking.Customer?.Address ?? "",

                City = booking.Customer?.City ?? "",

                State = booking.Customer?.State ?? "",

                Country = booking.Customer?.Country ?? "",

                Pincode = booking.Customer?.Pincode ?? "",
                IdentityType = booking.Customer?.IdentityType ?? "",
                IdentityNumber = booking.Customer?.IdentityNumber ?? "",
                IdentityImagePath = booking.Customer?.IdentityImagePath,
                EmergencyContactName = booking.Customer?.EmergencyContactName ?? "",
                EmergencyContactMobile = booking.Customer?.EmergencyContactMobile ?? "",

                Adults = booking.Adults ?? 0,

                Children = booking.Children ?? 0,

                CheckInDate = booking.CheckInDate,

                CheckOutDate = booking.CheckOutDate,

                TotalNights = booking.TotalNights,

                TotalAmount = booking.TotalAmount
            };

            ViewBag.BookingId = booking.Id;

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditBooking(long id, BookingVM model)
        {
            var booking = _context.Bookings
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == id);

            if (booking == null)
                return NotFound();

            booking.CheckInDate = model.CheckInDate;
            booking.CheckOutDate = model.CheckOutDate;

            booking.Adults = model.Adults;
            booking.Children = model.Children;

            booking.TotalNights =
                (model.CheckOutDate - model.CheckInDate).Days;

            booking.TotalAmount =
                booking.PricePerNight * booking.TotalNights;

            booking.Customer!.FullName = model.FullName;
            booking.Customer!.Mobile = model.Mobile;
            booking.Customer!.Email = model.Email;
            booking.Customer!.Gender = model.Gender;
            booking.Customer!.DateOfBirth = model.DateOfBirth;
            booking.Customer!.Address = model.Address;
            booking.Customer!.City = model.City;
            booking.Customer!.State = model.State;
            booking.Customer!.Country = model.Country;
            booking.Customer!.Pincode = model.Pincode;
            booking.Customer!.IdentityType = model.IdentityType;
            booking.Customer!.IdentityNumber = model.IdentityNumber;
            booking.Customer.IdentityImagePath = booking.Customer?.IdentityImagePath ?? "";
            booking.Customer!.EmergencyContactName = model.EmergencyContactName;
            booking.Customer!.EmergencyContactMobile = model.EmergencyContactMobile;


            booking.UpdatedOn = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction(nameof(ManageBooking));
        }



    }
}

