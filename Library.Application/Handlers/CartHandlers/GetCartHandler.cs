using AutoMapper;
using Library.Application.Commands.CartCommands;
using Library.Application.Dtos.CartDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.CartQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.CartHandlers
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, GetCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public GetCartHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<GetCartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new GetCartDto { Succeeded = false, Message = "Invalid Token" };

            var cart = await _unitOfWork.Carts.GatCartAsync(user.Id);
            
            if (cart == null) 
                return new GetCartDto { Succeeded = false, Message = "Invalid Token" };

            return _mapper.Map<GetCartDto>(cart);
        }
    }
}
