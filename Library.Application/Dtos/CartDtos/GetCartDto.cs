using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Application.Dtos.CartDtos
{
    public class GetCartDto
    {
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public decimal TotalCost { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonIgnore]
        public bool Succeeded { get; set; } = true;
    }
}
