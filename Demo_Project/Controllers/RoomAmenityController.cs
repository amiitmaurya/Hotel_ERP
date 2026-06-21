using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class RoomAmenityController : BaseController
    {
        private readonly HotelDbContext _context;

        public RoomAmenityController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: RoomAmenity
        public IActionResult Index()
        {
            var roomAmenities = _context.RoomAmenitys
                                        .Include(r => r.Room)
                                        .Include(r => r.Amenity)
                                        .ToList();

            return View(roomAmenities);
        }

        // GET: RoomAmenity/Create
        public IActionResult Create()
        {
            ViewBag.RoomId = new SelectList(
                _context.Rooms,
                "Id",
                "RoomNumber"
            );

            ViewBag.AmenityId = new SelectList(
                _context.Amenitys,
                "Id",
                "AmenityName"
            );

            return View();
        }

        // POST: RoomAmenity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomAmenity roomAmenity)
        {
            var exists = _context.RoomAmenitys.Any(x =>
        x.RoomId == roomAmenity.RoomId &&
        x.AmenityId == roomAmenity.AmenityId);

            if (exists)
            {
                ModelState.AddModelError("", "This amenity is already assigned to the room.");

                ViewBag.RoomId = new SelectList(_context.Rooms, "Id", "RoomNumber", roomAmenity.RoomId);
                ViewBag.AmenityId = new SelectList(_context.Amenitys, "Id", "AmenityName", roomAmenity.AmenityId);

                return View(roomAmenity);
            }

            _context.RoomAmenitys.Add(roomAmenity);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: RoomAmenity/Delete
        public IActionResult Delete(long roomId, long amenityId)
        {
            var roomAmenity = _context.RoomAmenitys
                                      .Include(r => r.Room)
                                      .Include(r => r.Amenity)
                                      .FirstOrDefault(x =>
                                          x.RoomId == roomId &&
                                          x.AmenityId == amenityId);

            if (roomAmenity == null)
            {
                return NotFound();
            }

            return View(roomAmenity);
        }

        // POST: RoomAmenity/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long roomId, long amenityId)
        {
            var roomAmenity = _context.RoomAmenitys
                                      .FirstOrDefault(x =>
                                          x.RoomId == roomId &&
                                          x.AmenityId == amenityId);

            if (roomAmenity != null)
            {
                _context.RoomAmenitys.Remove(roomAmenity);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
