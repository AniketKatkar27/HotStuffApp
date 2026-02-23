using HotStuffApp.Data;
using HotStuffApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var orders = _context.Orders.Where(o => o.UserId == user.UserId).ToList();

            return View(orders);
        }
    }
}
