using Microsoft.AspNetCore.Mvc;
using FirstCrudExample.Models;
using System.Linq;

namespace FirstCrudExample.Controllers
{
    public class UserlistController : Controller
    {
        private readonly LunarDbContext _context;

        public UserlistController(LunarDbContext context)
        {
            _context = context;
        }

        // =======================
        // REGISTER - GET
        // =======================
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        // =======================
        // REGISTER - POST
        // =======================
        [HttpPost]
        public IActionResult Register(RegisterModel registerData)
        {
            if (!ModelState.IsValid)
                return View(registerData);

            int newId = _context.Userlists.Any() ? _context.Userlists.Max(u => u.UserId) + 1 : 1;

            var user = new Userlist
            {
                UserId = newId,
                UserName = registerData.UserName,
                PhoneNumber = registerData.Phone,
                Email = registerData.Email,
                UserPassword = registerData.UserPassword,
                LoginStatus = false,
                UserTypeId = 1
            };

            _context.Userlists.Add(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Registration successful. Please login.";
            return RedirectToAction("Login", "Home");
        }
    }
}