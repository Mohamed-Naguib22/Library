using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public DateTime AddedOn { get; set; }
        public Cart Cart { get; set; }
        public Book Book { get; set; }
    }
}
