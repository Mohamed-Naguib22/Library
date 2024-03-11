using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.OrderDtos
{
    public class GetOrderItemDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookImageUrl { get; set; }
        public decimal SubTotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
