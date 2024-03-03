using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthDtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}