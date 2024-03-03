using Library.Application.Dtos.WishlistDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.WishlistQueries
{
    public class GetWishlistQuery : IRequest<GetWishlistDto>
    {
        public string RefreshToken { get; }
        public GetWishlistQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
