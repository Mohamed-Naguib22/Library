using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthDtos
{
    public class ForgetPasswordDto
    {
        [EmailAddress, StringLength(128)]
        public string Email { get; set; }
    }
}
