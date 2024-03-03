using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Library.Application.Dtos.AuthDtos
{
    public class TokenDto
    {
        public string? Token { get; set; }
        [JsonIgnore, ValidateNever]
        public string Message { get; set; }
    }
}
