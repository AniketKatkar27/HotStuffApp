using HotStuffApp.Data;
using HotStuffApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HotStuffApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly HotStuffAppDbContext _context;

        public OrderController(HotStuffAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            var session = HttpContext.Session.GetString("Cart");

            if (session == null)
                return RedirectToAction("Index", "Cart");

            var cart = JsonConvert.DeserializeObject<List<CartItem>>(session);

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            var userName = User.Identity?.Name;
            
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                return RedirectToAction("Login", "Account");

            decimal total = cart.Sum(item => item.ProductPrice * item.Quantity);

            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderTotal = total,
                UserId = user.UserId,
                OrderDetails = cart.Select(item => new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPrice = item.ProductPrice,
                    Quantity = item.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            HttpContext.Session.Remove("Cart");

            return View(order);
        }

        [Authorize]
        public IActionResult MyOrders()
        {
            var userName = User.Identity.Name;
            
            var user = _context.Users.FirstOrDefault(u => u.UserName ==userName);

            if (user == null)
                return Unauthorized();

            var orders = _context.Orders.Where(o => o.UserId == user.UserId).OrderByDescending(o => o.OrderDate).ToList();

            return View(orders);
        }

        //Order Details
        [Authorize]
        public async Task<IActionResult> OrderDetails(int id)
        {
            // Get logged-in user id from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            // Load order with items BUT only if it belongs to this user
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id && o.UserId == userId);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllOrders()
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

    }
}
