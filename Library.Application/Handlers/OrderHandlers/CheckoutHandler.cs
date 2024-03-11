using Library.Application.Commands.OrderCommands;
using Library.Application.Commands.RatingCommands;
using Library.Application.Dtos.OrderDtos;
using Library.Application.Interfaces;
using Library.Domain.Enums;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.OrderHandlers
{
    public class CheckoutHandler : IRequestHandler<CheckoutCommand, CheckoutResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IPaymentService _paymentService;
        public CheckoutHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _paymentService = paymentService;
        }
        public async Task<CheckoutResponse> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return new CheckoutResponse{ Succeeded = false, Message = "Invalid Token" };

            var totalCost = request.OrderItems.Sum(oi => oi.BookPrice * oi.Quantity);

            //foreach (var item in request.OrderItems)
            //{
            //    if (!await _unitOfWork.Books.ExistsAsync(b => b.Id == item.BookId));
            //        return new CheckoutResponse { Succeeded = false, Message = "Book is not found" };
            //}

            var order = new Order
            {
                ApplicationUser = user,
                PlacedOn = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalCost = totalCost
            };

            await _unitOfWork.Orders.AddAsync(order);

            foreach (var item in request.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    BookId= item.BookId,
                    Quantity = item.Quantity,
                    Order = order
                };
                await _unitOfWork.OrderItems.AddAsync(orderItem);
            }

            await _unitOfWork.CompleteAsync();

            var paymentLink = _paymentService.Checkout(order.Id, user, request.OrderItems);

            return new CheckoutResponse { PaymentLink = paymentLink };
        }
    }
}
