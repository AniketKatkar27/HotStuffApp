using HotStuffApp.Data;
using HotStuffApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.OrderDetails)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            return View(order);
        }

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

        public IActionResult ChangeStatus(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(OrderStatus)));

            return View(order);
        }
        [HttpPost]
        public IActionResult ChangeStatus(int id, OrderStatus status)
        {
            var order = _context.Orders.Find(id);

            if(order == null)
                return NotFound();

            order.Status = status;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
