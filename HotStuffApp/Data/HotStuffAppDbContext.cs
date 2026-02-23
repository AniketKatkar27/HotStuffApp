using HotStuffApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotStuffApp.Data
{
    public class HotStuffAppDbContext : DbContext
    {
        public HotStuffAppDbContext(DbContextOptions<HotStuffAppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Pizza" },
                new Category { CategoryId = 2, CategoryName = "Burger" },
                new Category { CategoryId = 3, CategoryName = "Beverages" },
                new Category { CategoryId = 4, CategoryName = "Desserts" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Margherita Pizza", ProductPrice = 299, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Farmhouse Pizza", ProductPrice = 399, CategoryId = 1 },
                new Product { ProductId = 3, ProductName = "Cheese Burger", ProductPrice = 149, CategoryId = 2 },
                new Product { ProductId = 4, ProductName = "Veg Supreme Burger", ProductPrice = 179, CategoryId = 2 },
                new Product { ProductId = 5, ProductName = "Coca Cola", ProductPrice = 49, CategoryId = 3 },
                new Product { ProductId = 6, ProductName = "Cold Coffee", ProductPrice = 99, CategoryId = 3 },
                new Product { ProductId = 7, ProductName = "Chocolate Brownie", ProductPrice = 129, CategoryId = 4 }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "Admin",
                    Email = "admin@hotstuff.com",
                    Password = "Admin@123", // For testing only
                    Role = "Admin"
                },
                new User
                {
                    UserId = 2,
                    UserName = "Aniket",
                    Email = "aniket@test.com",
                    Password = "123456",
                    Role = "Customer"
                }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    OrderDate = DateTime.Now,
                    OrderTotal = 448,
                    UserId = 2
                }
            );

            // Seed OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    OrderDetailId = 1,
                    OrderId = 1,
                    ProductId = 1,
                    ProductName = "Margherita Pizza",
                    ProductPrice = 299,
                    Quantity = 1
                },
                new OrderDetail
                {
                    OrderDetailId = 2,
                    OrderId = 1,
                    ProductId = 5,
                    ProductName = "Coca Cola",
                    ProductPrice = 49,
                    Quantity = 3
                }
            );
        }

    }
}
