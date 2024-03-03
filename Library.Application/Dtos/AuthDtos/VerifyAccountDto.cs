using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthDtos
{
    public class VerifyAccountDto
    {
        [EmailAddress, StringLength(128)]
        public string Email { get; set; }
        [StringLength(6, MinimumLength = 6)]
        public string VerificationCode { get; set; }
    }
}
