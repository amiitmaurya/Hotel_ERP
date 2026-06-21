using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class AmenityController : BaseController
    {
        private readonly HotelDbContext _context;

        public AmenityController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: Amenity
        public async Task<IActionResult> Index()
        {
            return View(await _context.Amenitys.ToListAsync());
        }

        // GET: Amenity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amenity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Amenity amenity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amenity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amenity);
        }

        // GET: Amenity/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var amenity = await _context.Amenitys.FindAsync(id);

            if (amenity == null)
                return NotFound();

            return View(amenity);
        }

        // POST: Amenity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Amenity amenity)
        {
            if (id != amenity.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(amenity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(amenity);
        }

        // GET: Amenity/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var amenity = await _context.Amenitys
                .FirstOrDefaultAsync(x => x.Id == id);

            if (amenity == null)
                return NotFound();

            return View(amenity);
        }

        // POST: Amenity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var amenity = await _context.Amenitys.FindAsync(id);

            if (amenity != null)
            {
                _context.Amenitys.Remove(amenity);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Amenity/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var amenity = await _context.Amenitys
                .FirstOrDefaultAsync(x => x.Id == id);

            if (amenity == null)
                return NotFound();

            return View(amenity);
        }
    }
}
