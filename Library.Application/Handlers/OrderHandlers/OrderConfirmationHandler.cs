using Library.Application.Commands.OrderCommands;
using Library.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.OrderHandlers
{
    public class OrderConfirmationHandler : IRequestHandler<OrderConfirmationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderConfirmationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(OrderConfirmationCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByAsync(o => o.Id == request.OrderId);
            
            if (order == null)
                return false;

            order.IsPaid = true;

            await _unitOfWork.CompleteAsync();
            
            return true;
        }
    }
}
