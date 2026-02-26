using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotStuffApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }   // NEW

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999)]
        public decimal ProductPrice { get; set; }

        public string? ImageUrl { get; set; }     // NEW

        // Foreign Key
        [Required(ErrorMessage ="Please Select a Category")]
        [Range(1, int.MaxValue, ErrorMessage ="Please Select a Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; } = null!;
    }
}