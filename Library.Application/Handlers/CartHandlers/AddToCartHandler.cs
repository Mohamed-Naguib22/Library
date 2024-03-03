using AutoMapper;
using Library.Application.Commands.CartCommands;
using Library.Application.Dtos.CartDtos;
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
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, CartItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public AddToCartHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<CartItemDto> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new CartItemDto { Succeeded = false, Message = "Invalid Token" };

            var book = await _unitOfWork.Books.GetByAsync(b => b.Id == request.AddToCartDto.BookId);

            if (book == null)
                return new CartItemDto { Succeeded = false, Message = "Book is not found" };

            var cartItem = new CartItem
            {
                CartId = user.Cart.Id,
                Quantity = 1,
                AddedOn = DateTime.UtcNow,
                Book = book
            };

            if (await _unitOfWork.CartItems.ExistsAsync(ci => ci.CartId == user.Cart.Id && ci.BookId == request.AddToCartDto.BookId))
                return new CartItemDto { Succeeded = false, Message = "This book is already added" };

            await _unitOfWork.CartItems.AddAsync(cartItem);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CartItemDto>(cartItem);
        }
    }
}
