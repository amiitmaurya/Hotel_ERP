using Demo_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly HotelDbContext _context;

        public AccountController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ViewBag.Roles = new SelectList(_context.Roles,
                                           "Id",
                                           "RoleName");

            return View();
        }

        [HttpPost]
        public IActionResult Register(SignupVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_context.Roles,
                                               "Id",
                                               "RoleName");

                return View("Login");
            }

            User user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Mobile = model.Mobile,
                UserName = model.UserName,
                PasswordHash = model.Password,
                RoleId = 10006,
                IsActive = true,
                CreatedOn = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration Successful";

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x =>
                x.UserName == model.UserName &&
                x.PasswordHash == model.Password);

            if (user == null)
            {
                ViewBag.LoginError = "Invalid Username Or Password";

                ViewBag.Roles = new SelectList(_context.Roles,
                                               "Id",
                                               "RoleName");

                return View();
            }
            // User Inactive Check
            if (!user.IsActive)
            {
                ViewBag.LoginError = "Your account is inactive. Please contact administrator.";


                return View();
            }


            HttpContext.Session.SetString("UserId",
                                          user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("Name", user.FullName);
            HttpContext.Session.SetString("RoleName", user.Role?.RoleName ?? "");
            HttpContext.Session.SetString("DashboardPermission",
    user.Role?.DashboardPermission.ToString() ?? "False");

            HttpContext.Session.SetString("BookingPermission",
                user.Role?.BookingPermission.ToString() ?? "False");

            HttpContext.Session.SetString("InvoicePermission",
                user.Role?.InvoicePermission.ToString() ?? "False");

            HttpContext.Session.SetString("UserPermission",
                user.Role?.UserPermission.ToString() ?? "False");

            HttpContext.Session.SetString("RoomPermission",
                user.Role?.RoomPermission.ToString() ?? "False");

            HttpContext.Session.SetString("ReportPermission",
                user.Role?.ReportPermission.ToString() ?? "False");

            HttpContext.Session.SetString("SettingPermission",
                user.Role?.SettingPermission.ToString() ?? "False");

            return RedirectToAction("Index",
                                    "Dashboard");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");

            Response.Headers["Cache-Control"] =
                "no-cache, no-store, must-revalidate";

            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            string? UserName = HttpContext.Session.GetString("UserName");

            if (UserName == null)
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);

            if (user == null)
                return RedirectToAction("Login");

            ChangePasswordVM vm = new ChangePasswordVM
            {
                UserName = user.UserName,
                OldPassword = user.PasswordHash
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM vm)
        {
            string? userName = HttpContext.Session.GetString("UserName");

            if (userName == null)
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return RedirectToAction("Login");

            vm.UserName = user.UserName;
            vm.OldPassword = user.PasswordHash;

            // New & Confirm Password Match
            if (vm.NewPassword != vm.ConfirmPassword)
            {
                TempData["Error"] = "New Password and Confirm Password must be same.";
                return View(vm);
            }

            // Old & New Password Should Not Be Same
            if (vm.NewPassword == user.PasswordHash)
            {
                TempData["Error"] = "New Password cannot be same as Old Password.";
                return View(vm);
            }

            user.PasswordHash = vm.NewPassword;

            _context.SaveChanges();
            HttpContext.Session.Clear();
            TempData["Success"] = "Password changed successfully. Redirecting to Login page in 10 seconds...";

            return View(vm);
        }



    }
}
