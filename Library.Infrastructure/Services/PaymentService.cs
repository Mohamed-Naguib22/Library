using Library.Application.Dtos.CartDtos;
using Library.Application.Dtos.OrderDtos;
using Library.Application.Interfaces;
using Library.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private const string DOMAIN = "https://localhost:7047/";
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Checkout(int orderId, ApplicationUser user, IEnumerable<OrderItemDto> orderItems)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = DOMAIN + $"api/Payment/OrderConfirmation?orderId={orderId}",
                CancelUrl = DOMAIN,
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = user.Email,
            };

            foreach (var item in orderItems)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.BookPrice * 100),
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.BookTitle,
                        }
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);

            return session.Url;
        }
    }
}
