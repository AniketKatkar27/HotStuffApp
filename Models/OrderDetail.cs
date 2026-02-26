using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotStuffApp.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999)]
        public decimal ProductPrice { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        // Foreign Key to Order
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}