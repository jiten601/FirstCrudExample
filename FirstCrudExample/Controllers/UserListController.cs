using FirstCrudExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.UserTypes = _context.Usertypes.ToList();
            return View(new RegisterModel());
        }

        // =======================
        // REGISTER - POST
        // =======================
        [HttpPost]
        public IActionResult Register(RegisterModel registerData, int UserTypeId)
        {
            ViewBag.UserTypes = _context.Usertypes.ToList();

            if (!ModelState.IsValid)
                return View(registerData);

            // Find the smallest missing UserId
            int newId = 1;
            var userIds = _context.Userlists
                                  .OrderBy(u => u.UserId)
                                  .Select(u => u.UserId)
                                  .ToList();

            foreach (var id in userIds)
            {
                if (id == newId)
                    newId++;
                else
                    break; // found the gap
            }


            var user = new Userlist
            {
                UserId = newId,
                UserName = registerData.UserName,
                PhoneNumber = registerData.Phone,
                Email = registerData.Email,
                UserPassword = registerData.UserPassword,
                LoginStatus = false,
                UserTypeId = UserTypeId // Assigned from dropdown
            };

            _context.Userlists.Add(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Registration successful. Please login.";
            return RedirectToAction("Login", "Home");
        }

        //Narbar Usertype
        [Authorize(Roles = "Admin")]
        public IActionResult UserByType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                TempData["ErrorMessage"] = "Invalid user type selected.";
                return RedirectToAction("Index", "Home");
            }

            var users = _context.Userlists
                .Include(u => u.UserType)
                .Where(u => u.UserType.TypeName == typeName)
                .ToList();

            ViewBag.UserTypeName = typeName;
            return View(users);
        }

    }
}
