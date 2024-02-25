using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthModels
{
    public class TokenrRequestModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}