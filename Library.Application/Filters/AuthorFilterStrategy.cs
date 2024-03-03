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
    public class AuthorFilterStrategy : IFilterStrategy<Book, BookFilterDto>
    {
        private readonly string? _author;
        public AuthorFilterStrategy(string? author)
        {
            _author = author?.ToLower();
        }
        public bool CanApply(BookFilterDto filter) => !string.IsNullOrEmpty(filter.Author);
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query) => query.Where(b => b.Author.Name.ToLower().Contains(_author));
    }
}
