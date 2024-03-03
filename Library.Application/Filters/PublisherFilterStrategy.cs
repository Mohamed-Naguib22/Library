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
    public class PublisherFilterStrategy : IFilterStrategy<Book, BookFilterDto>
    {
        private readonly string? _publisher;
        public PublisherFilterStrategy(string? publisher)
        {
            _publisher = publisher?.ToLower();
        }
        public bool CanApply(BookFilterDto filter) => !string.IsNullOrEmpty(filter.Publisher);
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query) => query.Where(b => b.Publisher.ToLower().Contains(_publisher));
    }
}
