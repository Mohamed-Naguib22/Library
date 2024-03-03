using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public DateTime TimeSpan { get; set; }
        public int BookId { get; set; }
        public string ApplicationUserId { get; set; }
        public Book Book { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
