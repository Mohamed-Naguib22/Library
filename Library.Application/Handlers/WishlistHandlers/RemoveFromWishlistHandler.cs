using Library.Application.Commands.CartCommands;
using Library.Application.Commands.WishlistCommands;
using Library.Application.Dtos.CartDtos;
using Library.Application.Dtos.WishlistDtos;
using Library.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.WishlistHandlers
{
    public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlistCommand, WishlistItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public RemoveFromWishlistHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<WishlistItemDto> Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new WishlistItemDto { Succeeded = false, Message = "Invalid Token" };

            if (!await _unitOfWork.Books.ExistsAsync(b => b.Id == request.BookId))
                return new WishlistItemDto { Succeeded = false, Message = "Book is not found" };

            var wishlistItem = await _unitOfWork.WishlistItems
                .GetByAsync(ci => ci.BookId == request.BookId && ci.WishlistId == user.Wishlist.Id);

            if (wishlistItem == null)
                return new WishlistItemDto { Succeeded = false, Message = "Book is added to wishlist" };

            _unitOfWork.WishlistItems.Delete(wishlistItem);
            await _unitOfWork.CompleteAsync();

            return new WishlistItemDto { Succeeded = true, Message = "Book removed successfully" };
        }
    }
}
