using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Attributes
{
    public class ExpirationDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsValidExpirationDate(value.ToString()))
                return new ValidationResult("Invalid expiration date");

            return ValidationResult.Success;
        }

        private static bool IsValidExpirationDate(string expirationDate)
        {
            if (!DateTime.TryParse(expirationDate, out DateTime expiry))
            {
                return false;
            }

            return expiry > DateTime.Now;
        }
    }
}
