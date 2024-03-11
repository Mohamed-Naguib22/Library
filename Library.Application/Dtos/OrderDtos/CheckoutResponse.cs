using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Application.Dtos.OrderDtos
{
    public class CheckoutResponse
    {
        public string PaymentLink { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonIgnore]
        public bool Succeeded { get; set; } = true;
    }
}
