using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class RoomController : BaseController
    {
        private readonly HotelDbContext _context;

        public RoomController(HotelDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Floor)
                .Include(r => r.RoomType)
                .ToListAsync();

            return View(rooms);
        }

        // CREATE GET
        public IActionResult Create()
        {
            ViewBag.FloorId = new SelectList(_context.Floors, "Id", "FloorName");
            ViewBag.RoomTypeId = new SelectList(_context.RoomTypes, "Id", "RoomTypeName");

            return View();
        }

        [HttpGet]
        public JsonResult GetRoomTypePrice(long roomTypeId)
        {
            var roomType = _context.RoomTypes
                .FirstOrDefault(x => x.Id == roomTypeId);

            if (roomType == null)
                return Json(0);

            return Json(roomType.BasePrice);
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                ViewBag.Errors = string.Join(" | ", errors);
                return View(room);
            }

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public async Task<IActionResult> Edit(long id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound();

            ViewBag.FloorId = new SelectList(_context.Floors, "Id", "FloorName", room.FloorId);
            ViewBag.RoomTypeId = new SelectList(_context.RoomTypes, "Id", "RoomTypeName", room.RoomTypeId);

            return View(room);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.FloorId = new SelectList(_context.Floors, "Id", "FloorName", room.FloorId);
            ViewBag.RoomTypeId = new SelectList(_context.RoomTypes, "Id", "RoomTypeName", room.RoomTypeId);

            return View(room);
        }

        // DELETE
        public async Task<IActionResult> Delete(long id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public async Task<IActionResult> Details(long id)
        {
            var room = await _context.Rooms
                .Include(r => r.Floor)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                return NotFound();

            return View(room);
        }
    }
}
