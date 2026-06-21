using Demo_Project.Models;
using Demo_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Project.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly HotelDbContext _context;

        public DashboardController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? checkInDate, DateTime? checkOutDate)
        {
            var model = new DashboardVM();

            model.CheckInDate = checkInDate;
            model.CheckOutDate = checkOutDate;

            model.TotalRooms = _context.Rooms.Count();

            model.Maintenance = _context.Bookings
                .Count(x => x.Status == BookingStatus.CheckedOut);

            model.ReservedRooms = _context.Bookings
                .Count(x => x.Status == BookingStatus.Booked);

            model.TodayCheckIns = _context.CheckIns
                .Count(x => x.CheckInDateTime.Date == DateTime.Today);

            model.TodayCheckOuts = _context.CheckOuts
                .Count(x => x.CheckOutDateTime.Date == DateTime.Today);

            // Default Available Rooms
            model.AvailableRooms = model.TotalRooms;

            if (checkInDate.HasValue && checkOutDate.HasValue)
            {
                var bookedRoomIds = _context.Bookings
                    .Where(b =>
                        b.Status != BookingStatus.Cancelled &&
                        checkInDate.Value < b.CheckOutDate &&
                        checkOutDate.Value > b.CheckInDate)
                    .Select(b => b.RoomId)
                    .Distinct()
                    .ToList();

                model.AvailableRooms = _context.Rooms
                    .Count(r => !bookedRoomIds.Contains(r.Id));
            }

            return View(model);
        }



    }
}
