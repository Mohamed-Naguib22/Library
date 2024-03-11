using Library.Application.Dtos.OrderDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.OrderQueries
{
    public class GetUserOrdersQuery : IRequest<IEnumerable<GetOrderDto>>
    {
        public string RefreshToken { get; }
        public GetUserOrdersQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
