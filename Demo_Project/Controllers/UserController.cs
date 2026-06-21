using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class UserController : BaseController
    {
        private readonly HotelDbContext _context;

        public UserController(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(x => x.Role)
                .ToListAsync();

            return View(users);
        }

        //GET
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_context.Roles, "Id", "RoleName");
            return View();
        }


        //POST
        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                var sessionUserId = HttpContext.Session.GetString("UserId");

                model.CreatedOn = DateTime.Now;

                if (!string.IsNullOrEmpty(sessionUserId))
                {
                    model.CreatedByUserId = Convert.ToInt64(sessionUserId);
                }

                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(_context.Roles, "Id", "RoleName");
            return View(model);
        }


        //GET
        public async Task<IActionResult> Edit(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            ViewBag.Roles = new SelectList(_context.Roles, "Id", "RoleName");

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);

                if (user == null)
                    return NotFound();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Mobile = model.Mobile;
                user.UserName = model.UserName;
                user.PasswordHash = model.PasswordHash;
                user.RoleId = model.RoleId;
                user.IsActive = model.IsActive;

                // Login User Id
                var sessionUserId = HttpContext.Session.GetString("UserId");

                if (!string.IsNullOrEmpty(sessionUserId))
                {
                    user.UpdatedByUserId = Convert.ToInt64(sessionUserId);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(_context.Roles, "Id", "RoleName");
            return View(model);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return RedirectToAction(nameof(Index));

            // Active users count
            var activeUsersCount = await _context.Users
                .CountAsync(x => x.IsActive);

            if (activeUsersCount <= 1)
            {
                TempData["Error"] = "At least one active user must remain in the system.";
                return RedirectToAction(nameof(Index));
            }

            user.IsActive = false;

            await _context.SaveChangesAsync();

            TempData["Success"] = "User deactivated successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
