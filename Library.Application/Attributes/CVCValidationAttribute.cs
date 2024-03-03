using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Attributes
{
    public class CVCValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsValidCVC(value.ToString()))
                return new ValidationResult("Invalid CVC");

            return ValidationResult.Success;
        }

        private static bool IsValidCVC(string cvc)
        {
            return !string.IsNullOrEmpty(cvc) && cvc.Length == 3 && int.TryParse(cvc, out _);
        }
    }
}
