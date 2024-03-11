using Library.Application.Dtos.OrderDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.OrderCommands
{
    public class CheckoutCommand : IRequest<CheckoutResponse>
    {
        public IEnumerable<OrderItemDto> OrderItems { get; }
        public string RefreshToken { get; }
        public CheckoutCommand(IEnumerable<OrderItemDto> orderItems, string refreshToken)
        {
            OrderItems = orderItems;
            RefreshToken = refreshToken;
        }
    }
}
