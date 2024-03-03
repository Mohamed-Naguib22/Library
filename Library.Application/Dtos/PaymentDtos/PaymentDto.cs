using Library.Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.PaymentDtos
{
    public class PaymentDto
    {
        [CreditCardNumberValidation(ErrorMessage = "Invalid credit card number")]
        public string CardNumber { get; set; }
        [CVCValidation(ErrorMessage = "Invalid CVC")]
        public string CVC { get; set; }
        [ExpirationDateValidation(ErrorMessage = "Invalid expiration date")]
        public string ExpirationDate { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Invalid amount")]
        public decimal Amount { get; set; }
    }
}
