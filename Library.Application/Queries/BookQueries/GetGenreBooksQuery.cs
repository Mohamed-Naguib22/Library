using Library.Application.Dtos.BookDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.BookQueries
{
    public class GetGenreBooksQuery : IRequest<IEnumerable<GetBooksDto>>
    {
        public int GenreId { get; }
        public GetGenreBooksQuery(int gernreId) 
        { 
            GenreId = gernreId;
        }
    }
}
