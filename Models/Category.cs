using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotStuffApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; } =string.Empty;

        public string? ImageUrl { get; set; }   

        // Navigation Property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}