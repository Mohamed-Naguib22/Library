using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        [Range(1, double.MaxValue)]
        public decimal BookPrice { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
