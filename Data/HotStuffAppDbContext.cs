using HotStuffApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotStuffApp.Data
{
    public class HotStuffAppDbContext : DbContext
    {
        public HotStuffAppDbContext(DbContextOptions<HotStuffAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // CATEGORY SEEDING (6)
            // =========================
            modelBuilder.Entity<Category>().HasData(

                new Category { CategoryId = 1, CategoryName = "Pizza", ImageUrl = "pizza.jpg" },
                new Category { CategoryId = 2, CategoryName = "Burgers", ImageUrl = "burger.jpg" },
                new Category { CategoryId = 3, CategoryName = "Indian", ImageUrl = "indian.jpg" },
                new Category { CategoryId = 4, CategoryName = "Chinese", ImageUrl = "chinese.jpg" },
                new Category { CategoryId = 5, CategoryName = "Beverages", ImageUrl = "beverages.jpg" },
                new Category { CategoryId = 6, CategoryName = "Desserts", ImageUrl = "desserts.jpg" }
            );

            // =========================
            // PRODUCT SEEDING (24)
            // =========================
            modelBuilder.Entity<Product>().HasData(

                // 🔴 PIZZA (1)
                new Product { ProductId = 1, ProductName = "Margherita Pizza", Description = "Classic cheese pizza", ProductPrice = 299, ImageUrl = "margherita.jpg", CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Farmhouse Pizza", Description = "Loaded with veggies", ProductPrice = 399, ImageUrl = "farmhouse.jpg", CategoryId = 1 },
                new Product { ProductId = 3, ProductName = "Peppy Paneer", Description = "Paneer & capsicum", ProductPrice = 379, ImageUrl = "paneer.jpg", CategoryId = 1 },
                new Product { ProductId = 4, ProductName = "Chicken Dominator", Description = "Loaded chicken pizza", ProductPrice = 449, ImageUrl = "chickenpizza.jpg", CategoryId = 1 },

                // 🔴 BURGERS (2)
                new Product { ProductId = 5, ProductName = "Veg Burger", Description = "Classic veg burger", ProductPrice = 149, ImageUrl = "vegburger.jpg", CategoryId = 2 },
                new Product { ProductId = 6, ProductName = "Cheese Burger", Description = "Extra cheese delight", ProductPrice = 179, ImageUrl = "cheeseburger.jpg", CategoryId = 2 },
                new Product { ProductId = 7, ProductName = "Chicken Burger", Description = "Crispy chicken patty", ProductPrice = 199, ImageUrl = "chickenburger.jpg", CategoryId = 2 },
                new Product { ProductId = 8, ProductName = "Double Patty Burger", Description = "Double layer burger", ProductPrice = 249, ImageUrl = "doubleburger.jpg", CategoryId = 2 },

                // 🔴 INDIAN (3)
                new Product { ProductId = 9, ProductName = "Butter Chicken", Description = "Creamy tomato gravy", ProductPrice = 349, ImageUrl = "butterchicken.jpg", CategoryId = 3 },
                new Product { ProductId = 10, ProductName = "Paneer Butter Masala", Description = "Rich paneer curry", ProductPrice = 299, ImageUrl = "paneerbutter.jpg", CategoryId = 3 },
                new Product { ProductId = 11, ProductName = "Hyderabadi Biryani", Description = "Spicy chicken biryani", ProductPrice = 329, ImageUrl = "biryani.jpg", CategoryId = 3 },
                new Product { ProductId = 12, ProductName = "Dal Tadka", Description = "Yellow dal with tadka", ProductPrice = 199, ImageUrl = "daltadka.jpg", CategoryId = 3 },

                // 🔴 CHINESE (4)
                new Product { ProductId = 13, ProductName = "Veg Hakka Noodles", Description = "Stir fried noodles", ProductPrice = 199, ImageUrl = "noodles.jpg", CategoryId = 4 },
                new Product { ProductId = 14, ProductName = "Chicken Fried Rice", Description = "Fried rice with chicken", ProductPrice = 229, ImageUrl = "friedrice.jpg", CategoryId = 4 },
                new Product { ProductId = 15, ProductName = "Veg Manchurian", Description = "Manchurian gravy balls", ProductPrice = 189, ImageUrl = "manchurian.jpg", CategoryId = 4 },
                new Product { ProductId = 16, ProductName = "Spring Rolls", Description = "Crispy rolls", ProductPrice = 169, ImageUrl = "springroll.jpg", CategoryId = 4 },

                // 🔴 BEVERAGES (5)
                new Product { ProductId = 17, ProductName = "Coca Cola", Description = "500ml chilled coke", ProductPrice = 60, ImageUrl = "coke.jpg", CategoryId = 5 },
                new Product { ProductId = 18, ProductName = "Cold Coffee", Description = "Chilled coffee", ProductPrice = 120, ImageUrl = "coldcoffee.jpg", CategoryId = 5 },
                new Product { ProductId = 19, ProductName = "Fresh Lime Soda", Description = "Refreshing lime soda", ProductPrice = 80, ImageUrl = "limesoda.jpg", CategoryId = 5 },
                new Product { ProductId = 20, ProductName = "Mango Shake", Description = "Fresh mango milkshake", ProductPrice = 140, ImageUrl = "mangoshake.jpg", CategoryId = 5 },

                // 🔴 DESSERTS (6)
                new Product { ProductId = 21, ProductName = "Chocolate Brownie", Description = "Hot chocolate brownie", ProductPrice = 149, ImageUrl = "brownie.jpg", CategoryId = 6 },
                new Product { ProductId = 22, ProductName = "Ice Cream Sundae", Description = "Vanilla chocolate sundae", ProductPrice = 129, ImageUrl = "sundae.jpg", CategoryId = 6 },
                new Product { ProductId = 23, ProductName = "Gulab Jamun", Description = "Traditional Indian sweet", ProductPrice = 99, ImageUrl = "gulabjamun.jpg", CategoryId = 6 },
                new Product { ProductId = 24, ProductName = "Cheesecake", Description = "Creamy cheesecake slice", ProductPrice = 179, ImageUrl = "cheesecake.jpg", CategoryId = 6 }
            );
        }
    }
}