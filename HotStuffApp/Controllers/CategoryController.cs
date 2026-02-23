using Microsoft.AspNetCore.Mvc;

namespace HotStuffApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
