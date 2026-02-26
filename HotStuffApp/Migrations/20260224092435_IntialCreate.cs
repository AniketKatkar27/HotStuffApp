using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotStuffApp.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Pizza", "Italian Style Pizzas" },
                    { 2, "Burgers", "Crispy & Juicy Burgers" },
                    { 3, "Indian", "Authentic Indian Cuisine" },
                    { 4, "Chinese", "Spicy Chinese Dishes" },
                    { 5, "Beverages", "Refreshing Drinks" },
                    { 6, "Desserts", "Sweet Treats" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ImageUrl", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "Classic cheese pizza", "margherita.jpg", "Margherita Pizza", 299m, 50 },
                    { 2, 1, "Loaded with veggies", "farmhouse.jpg", "Farmhouse Pizza", 399m, 40 },
                    { 3, 1, "Paneer & capsicum", "paneer.jpg", "Peppy Paneer", 379m, 35 },
                    { 4, 1, "Loaded chicken pizza", "chickenpizza.jpg", "Chicken Dominator", 449m, 30 },
                    { 5, 2, "Classic veg burger", "vegburger.jpg", "Veg Burger", 149m, 60 },
                    { 6, 2, "Extra cheese delight", "cheeseburger.jpg", "Cheese Burger", 179m, 50 },
                    { 7, 2, "Crispy chicken patty", "chickenburger.jpg", "Chicken Burger", 199m, 45 },
                    { 8, 2, "Double layer burger", "doubleburger.jpg", "Double Patty Burger", 249m, 30 },
                    { 9, 3, "Creamy tomato gravy", "butterchicken.jpg", "Butter Chicken", 349m, 25 },
                    { 10, 3, "Rich paneer curry", "paneerbutter.jpg", "Paneer Butter Masala", 299m, 25 },
                    { 11, 3, "Spicy chicken biryani", "biryani.jpg", "Hyderabadi Biryani", 329m, 40 },
                    { 12, 3, "Yellow dal with tadka", "daltadka.jpg", "Dal Tadka", 199m, 50 },
                    { 13, 4, "Stir fried noodles", "noodles.jpg", "Veg Hakka Noodles", 199m, 40 },
                    { 14, 4, "Fried rice with chicken", "friedrice.jpg", "Chicken Fried Rice", 229m, 35 },
                    { 15, 4, "Manchurian gravy balls", "manchurian.jpg", "Veg Manchurian", 189m, 30 },
                    { 16, 4, "Crispy rolls", "springroll.jpg", "Spring Rolls", 169m, 30 },
                    { 17, 5, "500ml chilled coke", "coke.jpg", "Coca Cola", 60m, 100 },
                    { 18, 5, "Chilled coffee", "coldcoffee.jpg", "Cold Coffee", 120m, 60 },
                    { 19, 5, "Refreshing lime soda", "limesoda.jpg", "Fresh Lime Soda", 80m, 70 },
                    { 20, 5, "Fresh mango milkshake", "mangoshake.jpg", "Mango Shake", 140m, 50 },
                    { 21, 6, "Hot chocolate brownie", "brownie.jpg", "Chocolate Brownie", 149m, 40 },
                    { 22, 6, "Vanilla chocolate sundae", "sundae.jpg", "Ice Cream Sundae", 129m, 35 },
                    { 23, 6, "Traditional Indian sweet", "gulabjamun.jpg", "Gulab Jamun", 99m, 50 },
                    { 24, 6, "Creamy cheesecake slice", "cheesecake.jpg", "Cheesecake", 179m, 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);
        }
    }
}
