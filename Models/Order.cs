using HotStuffApp.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotStuffApp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999)]
        public decimal OrderTotal { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;  // FIXED

        // Foreign Key to User
        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}