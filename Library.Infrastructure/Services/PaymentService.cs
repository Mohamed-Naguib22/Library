using Library.Application.Dtos.PaymentDtos;
using Library.Application.Interfaces;
using Stripe;
using Stripe.FinancialConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentService()
        {
        }
    }
}
