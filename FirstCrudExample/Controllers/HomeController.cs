using FirstCrudExample.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace FirstCrudExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly LunarDbContext _context;
        public HomeController(LunarDbContext context) { 
            _context=context;
        }
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // User is already logged in, redirect to Home/Index
                return RedirectToAction("Index", "Home");
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                Userlist? user = _context.Userlists
                    .Include(u => u.UserType)
                    .FirstOrDefault(u =>
                        u.PhoneNumber.Equals(login.LoginId) &&
                        u.UserPassword.Equals(login.UserPassword));

                if (user != null)
                {
                    user.LoginStatus = true;
                    await _context.SaveChangesAsync();

                    List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim("UserName", user.UserName),
                new Claim(ClaimTypes.Role, user.UserType.TypeName)
            };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // Set session timeout based on RememberMe
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,
                        ExpiresUtc = login.RememberMe
                            ? DateTimeOffset.UtcNow.AddDays(1)  // 1 day if RememberMe
                            : DateTimeOffset.UtcNow.AddMinutes(30) // 30 min if not
                    };

                    await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

                    return RedirectToAction(nameof(Index), nameof(Student));
                }
            }

            ModelState.AddModelError("", "Login failed. Try again.");
            return View(login);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction(nameof(Login));
        }
    }
}


