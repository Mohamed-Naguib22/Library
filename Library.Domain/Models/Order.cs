using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime PlacedOn { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsPaid { get; set; }
        [Range(0, double.MaxValue)]
        public decimal TotalCost { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
