using System.ComponentModel.DataAnnotations;

namespace Library.Application.Attributes
{
    public class CreditCardNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsValidCreditCardNumber(value.ToString()))
                return new ValidationResult("Invalid credit card number");

            return ValidationResult.Success;
        }
        private static bool IsValidCreditCardNumber(string creditCardNumber)
        {
            creditCardNumber = new string(creditCardNumber.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(creditCardNumber) || creditCardNumber.Length != 16)
                return false;

            int[] digits = creditCardNumber.Reverse().Select(c => c - '0').ToArray();

            for (int i = 1; i < digits.Length; i += 2)
            {
                digits[i] *= 2;

                if (digits[i] > 9)
                    digits[i] -= 9;
            }

            int sum = digits.Sum();

            return sum % 10 == 0;
        }
    }
}
