using Library.Application.Dtos.CartDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.CartCommands
{
    public class AddToCartCommand : IRequest<CartItemDto>
    {
        public AddToCartDto AddToCartDto { get; }
        public string RefreshToken { get; }
        public AddToCartCommand(AddToCartDto addToCartDto, string refreshToken)
        {
            AddToCartDto = addToCartDto;
            RefreshToken = refreshToken;
        }
    }
}
