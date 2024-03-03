using AutoMapper;
using Library.Application.Commands.CartCommands;
using Library.Application.Dtos.CartDtos;
using Library.Application.Dtos.WishlistDtos;
using Library.Application.Interfaces;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.CartHandlers
{
    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, CartItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public RemoveFromCartHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<CartItemDto> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new CartItemDto { Succeeded = false, Message = "Invalid Token" };

            if (!await _unitOfWork.Books.ExistsAsync(b => b.Id == request.BookId))
                return new CartItemDto { Succeeded = false, Message = "Book is not found" };

            var cartItem = await _unitOfWork.CartItems
                .GetByAsync(ci => ci.BookId == request.BookId && ci.CartId == user.Cart.Id);

            if (cartItem == null)
                return new CartItemDto { Succeeded = false, Message = "Book is added to cart" };

            _unitOfWork.CartItems.Delete(cartItem);
            await _unitOfWork.CompleteAsync();

            return new CartItemDto { Succeeded = true, Message = "Book removed successfully" };
        }
    }
}
