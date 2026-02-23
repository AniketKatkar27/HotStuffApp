using HotStuffApp.Data;
using HotStuffApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace HotStuffApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HotStuffAppDbContext _context;

        public AccountController(HotStuffAppDbContext context)
        {
            _context = context;
        }

        // REGISTER
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
           if(!ModelState.IsValid)
                return View(user);

           var existingUser = _context.Users.FirstOrDefault(u => u.UserName== user.UserName);

           if(existingUser != null)
            {
                ModelState.AddModelError("", "Username Already Exists.");
                return View(user);
            }

           user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Role = "Customer";

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // LOGIN
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password,user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid Username or Password";
            return View();
        }

        // LOGOUT
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
