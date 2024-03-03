using Library.Application.Dtos.WishlistDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.WishlistCommands
{
    public class RemoveFromWishlistCommand : IRequest<WishlistItemDto>
    {
        public int BookId { get; }
        public string RefreshToken { get; }
        public RemoveFromWishlistCommand(int bookId, string refreshToken)
        {
            BookId = bookId;
            RefreshToken = refreshToken;
        }
    }
}
