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
            return View(); // Looks for Views/Home/Index.cshtml
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                Userlist? user = _context.Userlists.Include(u=>u.UserType).Where(u=>u.LoginStatus==true && u.PhoneNumber.Equals(login.LoginId) && u.UserPassword.Equals(login.UserPassword)).FirstOrDefault();
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.UserId.ToString()),
                        new Claim("UserName",user.UserName),
                        new Claim(ClaimTypes.Role,user.UserType.TypeName)
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims,"MyCookieAuth");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("MyCookieAuth",principal, new AuthenticationProperties { IsPersistent=true});
                    return RedirectToAction(nameof(Index),nameof(Student));
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


