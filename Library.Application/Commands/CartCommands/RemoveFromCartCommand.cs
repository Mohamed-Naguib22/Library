using Library.Application.Dtos.CartDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.CartCommands
{
    public class RemoveFromCartCommand : IRequest<CartItemDto>
    {
        public int BookId { get; }
        public string RefreshToken { get; }
        public RemoveFromCartCommand(int bookId, string refreshToken)
        {
            BookId = bookId;
            RefreshToken = refreshToken;
        }
    }
}
