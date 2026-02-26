using HotStuffApp.Data;
using HotStuffApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotStuffApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly HotStuffAppDbContext _context;

        public HomeController(HotStuffAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public async Task<IActionResult> ProductsByCategory(int id, string search)
        {
            var products = _context.Products
                .Where(p => p.CategoryId == id);

            if (!string.IsNullOrEmpty(search))
            {
                products = products
                    .Where(p => p.ProductName.Contains(search));
            }

            ViewBag.CurrentCategory = id;

            return View(await products.ToListAsync());
        }

        public async Task<IActionResult> Products(string search)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // Search filter
            if (!string.IsNullOrEmpty(search))
            {
                products = products
                    .Where(p => p.ProductName.ToLower().Contains(search));
            }

            return View(await products.ToListAsync());
        }

        // ================= ABOUT US =================
        public IActionResult AboutUs()
        {
            return View();
        }

        // ================= CONTACT US =================
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
