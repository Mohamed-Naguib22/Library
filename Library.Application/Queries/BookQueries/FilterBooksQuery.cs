using Library.Application.Dtos.BookDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.BookQueries
{
    public class FilterBooksQuery : IRequest<IEnumerable<GetBooksDto>>
    {
        public BookFilterDto BookFilterDto { get; }
        public FilterBooksQuery(BookFilterDto bookFilterDto)
        {
            BookFilterDto = bookFilterDto;
        }
    }
}
