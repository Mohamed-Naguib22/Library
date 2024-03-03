using Library.Application.Dtos.BookDtos;
using Library.Application.Interfaces;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Filters
{
    public class GenreFilterStrategy : IFilterStrategy<Book, BookFilterDto>
    {
        private readonly string? _genre;
        public GenreFilterStrategy(string? genre)
        {
            _genre = genre?.ToLower();
        }
        public bool CanApply(BookFilterDto filter) => !string.IsNullOrEmpty(filter.Genre);
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query) => 
            query.Where(b => b.BookGenres.Any(bg => bg.Genre.Name.ToLower().Contains(_genre)));
    }
}
