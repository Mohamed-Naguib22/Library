using Library.Application.Dtos.BookDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.BookQueries
{
    public class SearchForBookQuery : IRequest<IEnumerable<GetBooksDto>>
    {
        public string SearchQuery { get; }
        public string? RefreshToken { get; }
        public SearchForBookQuery(string searchQuery, string? refreshToken)
        {
            SearchQuery = searchQuery;
            RefreshToken = refreshToken;
        }
    }
}
