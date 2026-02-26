using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotStuffApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Role { get; set; } = "Customer";

        [Phone]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }   // NEW

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}