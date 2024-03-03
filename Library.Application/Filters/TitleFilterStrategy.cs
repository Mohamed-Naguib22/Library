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
    public class TitleFilterStrategy : IFilterStrategy<Book, BookFilterDto>
    {
        private readonly string? _title;
        public TitleFilterStrategy(string? title)
        {
            _title = title?.ToLower();
        }
        public bool CanApply(BookFilterDto filter) =>!string.IsNullOrEmpty(filter.Title);
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query) => query.Where(b => b.Title.ToLower().Contains(_title));
    }
}
