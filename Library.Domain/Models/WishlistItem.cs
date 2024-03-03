using System.Text.Json.Serialization;

namespace Library.Domain.Models
{
    public class WishlistItem
    {
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public DateTime AddedOn { get; set; }
        [JsonIgnore]
        public Wishlist Wishlist { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
    }
}