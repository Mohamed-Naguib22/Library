using AutoMapper;
using Library.Application.Commands.CartCommands;
using Library.Application.Commands.WishlistCommands;
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

namespace Library.Application.Handlers.WishlistHandlers
{
    public class AddToWishlistHandler : IRequestHandler<AddToWishlistCommand, WishlistItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public AddToWishlistHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<WishlistItemDto> Handle(AddToWishlistCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new WishlistItemDto { Succeeded = false, Message = "Invalid Token" };

            var book = await _unitOfWork.Books.GetByAsync(b => b.Id == request.AddToWishlistDto.BookId);

            if (book == null)
                return new WishlistItemDto { Succeeded = false, Message = "Book is not found" };

            var wihlistItem = new WishlistItem
            {
                WishlistId = user.Wishlist.Id,
                Book = book,
                AddedOn = DateTime.Now
            };

            if (await _unitOfWork.WishlistItems.ExistsAsync(wi => wi.WishlistId == user.Cart.Id && wi.BookId == request.AddToWishlistDto.BookId))
                return new WishlistItemDto { Succeeded = false, Message = "This book is already added" };

            await _unitOfWork.WishlistItems.AddAsync(wihlistItem);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<WishlistItemDto>(wihlistItem);
        }
    }
}
