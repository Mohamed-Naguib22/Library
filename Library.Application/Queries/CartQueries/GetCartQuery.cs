using Library.Application.Dtos.CartDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.CartQueries
{
    public class GetCartQuery : IRequest<GetCartDto>
    {
        public string RefreshToken { get; }
        public GetCartQuery(string refreshToken) 
        {
            RefreshToken = refreshToken;
        }
    }
}
