using System.ComponentModel.DataAnnotations;

namespace HotStuffApp.Models
{
    public class CartItem
    {
        [Required]
        public int ProductId { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(0.01,999999)]
        public decimal ProductPrice { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
    }
}