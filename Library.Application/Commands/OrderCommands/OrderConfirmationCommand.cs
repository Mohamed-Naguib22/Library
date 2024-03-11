using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.OrderCommands
{
    public class OrderConfirmationCommand : IRequest<bool>
    {
        public int OrderId { get; }
        public OrderConfirmationCommand(int orderId) 
        {
            OrderId = orderId;
        }
    }
}
