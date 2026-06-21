using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class FloorController : BaseController
    {
        private readonly HotelDbContext _context;

        public FloorController(HotelDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var floors = await _context.Floors.ToListAsync();
            return View(floors);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Floor floor)
        {
            if (ModelState.IsValid)
            {
                _context.Floors.Add(floor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(floor);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(long id)
        {
            var floor = await _context.Floors.FindAsync(id);

            if (floor == null)
                return NotFound();

            return View(floor);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Floor floor)
        {
            if (ModelState.IsValid)
            {
                _context.Floors.Update(floor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(floor);
        }

        // DETAILS
        public async Task<IActionResult> Details(long id)
        {
            var floor = await _context.Floors
                .FirstOrDefaultAsync(x => x.Id == id);

            if (floor == null)
                return NotFound();

            return View(floor);
        }

        // DELETE
        public async Task<IActionResult> Delete(long id)
        {
            var floor = await _context.Floors.FindAsync(id);

            if (floor == null)
                return NotFound();

            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
