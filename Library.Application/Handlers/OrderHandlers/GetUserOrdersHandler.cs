using AutoMapper;
using Library.Application.Commands.OrderCommands;
using Library.Application.Dtos.OrderDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.OrderQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.OrderHandlers
{
    public class GetUserOrdersHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<GetOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public GetUserOrdersHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetOrderDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            var orders = await _unitOfWork.Orders.GetUserOrders(user.Id);

            return _mapper.Map<IEnumerable<GetOrderDto>>(orders);
        }
    }
}
