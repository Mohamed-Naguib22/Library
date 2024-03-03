using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.UserQueries
{
    public class GetSearchHistoryQuery : IRequest<IEnumerable<SearchQuery>>
    {
        public string RefreshToken { get; }
        public GetSearchHistoryQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
