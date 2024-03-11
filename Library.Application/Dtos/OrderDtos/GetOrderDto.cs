using Library.Domain.Enums;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Application.Dtos.OrderDtos
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string PlacedOn { get; set; }
        public string Status { get; set; }
        public decimal TotalCost { get; set; }
        public IEnumerable<GetOrderItemDto> OrderItems { get; set; }
    }
}
