using Library.Application.Dtos.BookDtos;
using Library.Application.Filters;
using Library.Application.Interfaces;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class BookFilterFactory : IFilterFactory<Book, BookFilterDto>
    {
        public IEnumerable<IFilterStrategy<Book, BookFilterDto>> CreateFilterStrategies(BookFilterDto filter)
        {
            var strategies = new List<IFilterStrategy<Book, BookFilterDto>>
            {
                new TitleFilterStrategy(filter.Title),
                new PublicationDateFilterStrategy(filter.PublicationDate),
                new LanguageFilterStrategy(filter.Language),
                new PriceFilterStrategy(filter.Price),
                new GenreFilterStrategy(filter.Genre),
                new AuthorFilterStrategy(filter.Author),
                new PublisherFilterStrategy(filter.Publisher)
            };
            return strategies.Where(strategy => strategy.CanApply(filter));
        }
    }
}
