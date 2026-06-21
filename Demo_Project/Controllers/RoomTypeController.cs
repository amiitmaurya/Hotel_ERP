using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class RoomTypeController : BaseController
    {
        private readonly HotelDbContext _context;

        public RoomTypeController(HotelDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var roomTypes = await _context.RoomTypes.ToListAsync();
            return View(roomTypes);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                _context.RoomTypes.Add(roomType);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(roomType);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(long id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
                return NotFound();

            return View(roomType);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                _context.RoomTypes.Update(roomType);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(roomType);
        }

        // DETAILS
        public async Task<IActionResult> Details(long id)
        {
            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (roomType == null)
                return NotFound();

            return View(roomType);
        }

        // DELETE
        public async Task<IActionResult> Delete(long id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
                return NotFound();

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
