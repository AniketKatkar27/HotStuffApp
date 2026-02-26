using HotStuffApp.Data;
using HotStuffApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotStuffApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly HotStuffAppDbContext _context;

        public CartController(HotStuffAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound(); 
            
            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(p => p.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl
                });
            }

            SaveCart(cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if(item != null)
            {
                cart.Remove(item);
            }

            SaveCart(cart);

            return RedirectToAction("Index");
        }

        private List<CartItem> GetCart()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");

            if (sessionCart == null)
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString("Cart",
                JsonConvert.SerializeObject(cart));
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(int id)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                item.Quantity++;
            }

            SaveCart(cart);   // VERY IMPORTANT

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int id)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                    cart.Remove(item);
            }

            SaveCart(cart);   // VERY IMPORTANT

            return RedirectToAction("Index");
        }
    }
}
