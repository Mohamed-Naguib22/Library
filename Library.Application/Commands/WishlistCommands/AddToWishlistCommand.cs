using Library.Application.Dtos.WishlistDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.WishlistCommands
{
    public class AddToWishlistCommand : IRequest<WishlistItemDto>
    {
        public AddToWishlistDto AddToWishlistDto { get; }
        public string RefreshToken { get; }
        public AddToWishlistCommand(AddToWishlistDto addToWishlistDto, string refreshToken)
        {
            AddToWishlistDto = addToWishlistDto;
            RefreshToken = refreshToken;
        }
    }
}
