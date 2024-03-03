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
    public class PublicationDateFilterStrategy : IFilterStrategy<Book, BookFilterDto>
    {
        private readonly string? _publicationDate;
        public PublicationDateFilterStrategy(string? publicationDate)
        {
            _publicationDate = publicationDate;
        }
        public bool CanApply(BookFilterDto filter) => !string.IsNullOrEmpty(filter.PublicationDate);
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query) => 
            query.Where(b => b.PublicationDate.Year.ToString() == _publicationDate);
    }
}
