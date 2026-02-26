using HotStuffApp.Data;
using HotStuffApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Authorize(Roles = "Admin")]
public class AdminDashboardController : Controller
{
    private readonly HotStuffAppDbContext _context;

    public AdminDashboardController(HotStuffAppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var dashboard = new AdminDashboardVM
        {
            TotalOrders = _context.Orders.Count(),
            TotalProducts = _context.Products.Count(),
            TotalCategories = _context.Categories.Count(),
            TotalCustomers = _context.Users.Count(u => u.Role == "Customer"),
            TotalRevenue = _context.Orders.Sum(o => (decimal?)o.OrderTotal) ?? 0,

            RecentOrders = _context.Orders.OrderByDescending(o => o.OrderDate).Take(5).ToList()
        };
        


        return View(dashboard);
    }
}
