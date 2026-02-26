using HotStuffApp.Data;
using HotStuffApp.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace HotStuffApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly HotStuffAppDbContext _context;

        public AdminOrdersController(HotStuffAppDbContext context)
        {
            _context = context;
        }

        // ===================== INDEX WITH SEARCH + FILTER =====================
        public IActionResult Index(string searchString, OrderStatus? statusFilter)
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .AsQueryable();

            // Search by customer name
            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.User.UserName.Contains(searchString));
            }

            // Filter by status
            if (statusFilter.HasValue)
            {
                orders = orders.Where(o => o.Status == statusFilter.Value);
            }

            ViewBag.StatusList = new SelectList(
                Enum.GetValues(typeof(OrderStatus)));

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentStatus = statusFilter;

            return View(orders.OrderByDescending(o => o.OrderDate).ToList());
        }

        // ===================== INLINE STATUS UPDATE =====================
        [HttpPost]
        public IActionResult UpdateStatus(int id, OrderStatus status)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            order.Status = status;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ===================== DELETE =====================
        public IActionResult Delete(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //Change Status
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}