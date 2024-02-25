using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDtos
{
    public class UpdateBookDto
    {
        [StringLength(256)]
        public string? Title { get; set; }
        [StringLength(13, MinimumLength = 13)]
        public string? ISBN { get; set; }
        [StringLength(256)]
        public string? Publisher { get; set; }
        [StringLength(256)]
        public string? Language { get; set; }
        [StringLength(1024)]
        public string? Summary { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
        [Range(0, int.MaxValue)]
        public int? NumOfPages { get; set; }
        [Range(0, int.MaxValue)]
        public int? NumOfCopiesInStock { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime LastUpdatedAt { get; } = DateTime.Now;
        public int? AuthorId { get; set; }
    }
}
