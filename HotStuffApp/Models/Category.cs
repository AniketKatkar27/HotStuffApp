using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotStuffApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        // Navigation Property
        public List<Product> Products { get; set; }
    }
}