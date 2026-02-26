using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotStuffApp.Data;
using HotStuffApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotStuffApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly HotStuffAppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(HotStuffAppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ================= INDEX =================
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();

            return View(products);
        }

        // ================= DETAILS =================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // ================= CREATE =================
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync(product.CategoryId);
                return View(product);
            }

            // Validate Category Exists
            bool categoryExists = await _context.Categories
                .AnyAsync(c => c.CategoryId == product.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError("CategoryId", "Invalid category selected.");
                await LoadCategoriesAsync(product.CategoryId);
                return View(product);
            }

            if (imageFile != null && imageFile.Length > 0){
                product.ImageUrl = await SaveImageAsync(imageFile);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================= EDIT =================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            await LoadCategoriesAsync(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
        {
            if (id != product.ProductId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync(product.CategoryId);
                return View(product);
            }

            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return NotFound();

            existing.ProductName = product.ProductName;
            existing.Description = product.Description;
            existing.ProductPrice = product.ProductPrice;
            existing.CategoryId = product.CategoryId;

            if (imageFile != null && imageFile.Length > 0)
                existing.ImageUrl = await SaveImageAsync(imageFile);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================= DELETE =================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ================= HELPERS =================
        private async Task LoadCategoriesAsync(int? selectedId = null)
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            ViewBag.CategoryList = new SelectList(
                categories,
                "CategoryId",
                "CategoryName",
                selectedId);
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            string folder = Path.Combine(_env.WebRootPath, "images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }
    }
}