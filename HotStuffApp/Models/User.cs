using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotStuffApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)] // BCrypt safe length
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        // Navigation Property
        public List<Order> Orders { get; set; }
    }
}