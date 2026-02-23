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
        var dashboard = new AdminDashboardVM();

        dashboard.TotalOrders = _context.Orders?.Count() ?? 0;
        dashboard.TotalProducts = _context.Products?.Count() ?? 0;
        dashboard.TotalCategories = _context.Categories?.Count() ?? 0;

        dashboard.TotalCustomers = _context.Users.Count(u => u.Role == "Customer");


        // Safe revenue calculation
        dashboard.TotalRevenue = _context.Orders != null && _context.Orders.Any()
            ? _context.Orders.Sum(o => o.OrderTotal)
            : 0;


        return View(dashboard);
    }
}
