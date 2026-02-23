using HotStuffApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotStuffApp.Controllers
{
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

    }
}
