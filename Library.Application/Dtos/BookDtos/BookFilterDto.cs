using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.BookDtos
{
    public class BookFilterDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? Language { get; set; }
        public string? Genre { get; set; }
        public string? PublicationDate { get; set; }
        public decimal? Price { get; set; }
    }
}
