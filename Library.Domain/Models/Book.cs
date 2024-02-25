using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    [Index(nameof(Title), IsUnique = true)]
    [Index(nameof(ISBN), IsUnique = true)]
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(256)]
        public string Title { get; set; }
        [StringLength(13, MinimumLength = 13)]
        public string ISBN { get; set; }
        [MaxLength(256)]
        public string Publisher { get; set; }
        [MaxLength(256)]
        public string Language { get; set; }
        [MaxLength(1024)]
        public string Summary { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int NumOfPages { get; set; }
        [Range(0, int.MaxValue)]
        public int NumOfCopiesInStock { get; set; }
        public string ImgUrl { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public IEnumerable<BookGenre> BookGenres { get; set; }
    }
}
