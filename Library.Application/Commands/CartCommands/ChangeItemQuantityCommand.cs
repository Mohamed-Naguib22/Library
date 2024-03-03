using Library.Application.Dtos.CartDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.CartCommands
{
    public class ChangeItemQuantityCommand : IRequest<CartItemDto>
    {
        public ChangeQuantityDto UpdateQuantityDto { get; }
        public string RefreshToken { get; }
        public ChangeItemQuantityCommand(ChangeQuantityDto updateQuantityDto, string refreshToken)
        {
            UpdateQuantityDto = updateQuantityDto;
            RefreshToken = refreshToken;
        }
    }
}
