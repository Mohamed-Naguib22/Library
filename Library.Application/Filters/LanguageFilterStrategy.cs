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
    public class LanguageFilterStrategy : IFilterStrategy<Book, BookFilterDto>
    {
        private readonly string? _language;
        public LanguageFilterStrategy(string? language)
        {
            _language = language?.ToLower();
        }
        public bool CanApply(BookFilterDto filter) => !string.IsNullOrEmpty(filter.Language);
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query) => query.Where(b => b.Language.ToLower().Contains(_language));

    }
}
