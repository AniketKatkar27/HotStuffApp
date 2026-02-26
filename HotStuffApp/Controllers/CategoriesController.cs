using HotStuffApp.Data;
using HotStuffApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace HotStuffApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoriesController : Controller
    {
        private readonly HotStuffAppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(HotStuffAppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null) return NotFound();

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Category category, IFormFile? imageFile)
        //{
        //    if (ModelState.IsValid)
        //        return View(category);

        //    if (imageFile != null)
        //    {
        //        category.ImageUrl = await SaveImageAsync(imageFile);
        //    }

        //        _context.Categories.Add(category);
        //        await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));

        //}
        // ================= CREATE =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(category);

            if (imageFile != null && imageFile.Length > 0)
            {
                category.ImageUrl = await SaveImageAsync(imageFile);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Category category, IFormFile? imageFile)
        //{
        //    if (id != category.CategoryId)
        //        return NotFound();

        //    if (ModelState.IsValid)
        //        return View(category);

        //    var existingCategory = await _context.Categories.FindAsync(id);
        //    if (existingCategory == null) return NotFound();

        //    existingCategory.CategoryName = category.CategoryName;

        //    if (imageFile != null)
        //    {
        //        existingCategory.ImageUrl = await SaveImageAsync(imageFile);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));

        //}
        // ================= EDIT =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category, IFormFile? imageFile)
        {
            if (id != category.CategoryId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(category);

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.CategoryName = category.CategoryName;

            if (imageFile != null && imageFile.Length > 0)
            {
                existingCategory.ImageUrl = await SaveImageAsync(imageFile);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return RedirectToAction(nameof(Index));

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        //Image Helper
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return fileName;
        }
    }
}