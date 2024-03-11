using Library.Application.Dtos.OrderDtos;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IPaymentService
    {
        public string Checkout(int orderId, ApplicationUser user, IEnumerable<OrderItemDto> orderItems);
    }
}
