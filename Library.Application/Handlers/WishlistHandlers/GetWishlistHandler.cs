using AutoMapper;
using Library.Application.Commands.WishlistCommands;
using Library.Application.Dtos.CartDtos;
using Library.Application.Dtos.WishlistDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.WishlistQueries;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.WishlistHandlers
{
    public class GetWishlistHandler : IRequestHandler<GetWishlistQuery, GetWishlistDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public GetWishlistHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<GetWishlistDto> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new GetWishlistDto { Succeeded = false, Message = "Invalid Token" };

            var wishlist = await _unitOfWork.Wishlists.GetWishlistAsync(user.Id);

            if (wishlist == null)
                return new GetWishlistDto { Succeeded = false, Message = "Invalid Token" };

            return _mapper.Map<GetWishlistDto>(wishlist);
        }
    }
}
