using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class RoomImageController : BaseController
    {
        private readonly HotelDbContext _context;
        private readonly IWebHostEnvironment _env;

        public RoomImageController(
            HotelDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // LIST
        public IActionResult Index()
        {
            var images = _context.RoomImages
                .Include(x => x.Room)
                .ToList();

            return View(images);
        }

        // CREATE GET
        public IActionResult Create(long roomId)
        {
            RoomImageVM vm = new RoomImageVM();

            vm.Rooms = _context.Rooms
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.RoomNumber
                })
                .ToList();

            return View(vm);
        }


        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomImageVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Rooms = _context.Rooms
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.RoomNumber
                    })
                    .ToList();

                return View(model);
            }

            string folder = Path.Combine(_env.WebRootPath, "RoomImages");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() +
                              Path.GetExtension(model.ImageFile.FileName);

            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            RoomImage image = new RoomImage
            {
                RoomId = model.RoomId,
                ImagePath = "/RoomImages/" + fileName,
                IsPrimary = model.IsPrimary
            };

            _context.RoomImages.Add(image);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public IActionResult Edit(long id)
        {
            var image = _context.RoomImages
                .FirstOrDefault(x => x.Id == id);

            if (image == null)
                return NotFound();

            return View(image);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomImage model, IFormFile? NewImage)
        {
            var image = await _context.RoomImages.FindAsync(model.Id);

            if (image == null)
                return NotFound();

            image.IsPrimary = model.IsPrimary;

            if (model.IsPrimary)
            {
                var images = _context.RoomImages
                    .Where(x => x.RoomId == image.RoomId);

                foreach (var item in images)
                {
                    item.IsPrimary = false;
                }

                image.IsPrimary = true;
            }

            if (NewImage != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "RoomImages");

                string fileName = Guid.NewGuid() +
                    Path.GetExtension(NewImage.FileName);

                string path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await NewImage.CopyToAsync(stream);
                }

                image.ImagePath = "/RoomImages/" + fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE GET
        public IActionResult Delete(long id)
        {
            var image = _context.RoomImages.Find(id);

            if (image == null)
                return NotFound();

            return View(image);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var image = await _context.RoomImages.FindAsync(id);

            if (image == null)
                return NotFound();

            string filePath = Path.Combine(
                _env.WebRootPath,
                image.ImagePath.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            long roomId = image.RoomId;

            _context.RoomImages.Remove(image);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
