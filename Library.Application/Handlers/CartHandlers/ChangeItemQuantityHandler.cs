using AutoMapper;
using Library.Application.Commands.CartCommands;
using Library.Application.Dtos.CartDtos;
using Library.Application.Interfaces;
using Library.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.CartHandlers
{
    public class ChangeItemQuantityHandler : IRequestHandler<ChangeItemQuantityCommand, CartItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public ChangeItemQuantityHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<CartItemDto> Handle(ChangeItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var updateDto = request.UpdateQuantityDto;

            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new CartItemDto { Succeeded = false, Message = "Invalid Token" };

            if (!await _unitOfWork.Books.ExistsAsync(b => b.Id == request.UpdateQuantityDto.BookId))
                return new CartItemDto { Succeeded = false, Message = "Book is not found" };

            var cartItem = await _unitOfWork.Carts.GetCartItem(updateDto.BookId, user.Cart.Id);

            if (cartItem == null)
                return new CartItemDto { Succeeded = false, Message = "Book is added to cart" };

            if (updateDto.ChangeQuantity == Quantity.Up.ToString())
                cartItem.Quantity++;

            else if (updateDto.ChangeQuantity == Quantity.Down.ToString())
            {
                if (cartItem.Quantity <= 1)
                    return new CartItemDto { Succeeded = false, Message = "The Quantity can not be zero" };

                cartItem.Quantity--;
            }

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CartItemDto>(cartItem);
        }
    }
}
