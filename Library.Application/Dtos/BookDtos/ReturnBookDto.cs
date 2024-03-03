using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDtos
{
    public class ReturnBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int NumOfPages { get; set; }
        public bool IsAvailable { get; set; }
        public string ImgUrl { get; set; }
        public string PublicationDate { get; set; }
        public string Author { get; set; }
        public IEnumerable<string> BookGenres { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonIgnore]
        public bool Succeeded { get; set; } = true;
    }

}
