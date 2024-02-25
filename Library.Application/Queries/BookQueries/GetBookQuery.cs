using Library.Application.Dtos.BookDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.BookQueries
{
    public class GetBookQuery : IRequest<GetBookDto>
    {
        public int BookId { get; }
        public GetBookQuery(int bookId)
        {
            BookId = bookId;
        }
    }
}
