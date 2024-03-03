using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Application.Dtos.CartDtos
{
    public class CartItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public string BookTitle { get; set; }
        public string BookImageUrl { get; set; }
        public string AddedOn { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonIgnore]
        public bool Succeeded { get; set; } = true;
    }
}
