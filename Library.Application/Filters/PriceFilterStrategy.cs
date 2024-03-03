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
    public class PriceFilterStrategy : IFilterStrategy<Book, BookFilterDto>
    {
        private readonly decimal? _price;
        public PriceFilterStrategy(decimal? price)
        {
            _price = price;
        }
        public bool CanApply(BookFilterDto filter) => filter.Price.HasValue;
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query) => query.Where(b => b.Price <= _price);
    }
}
