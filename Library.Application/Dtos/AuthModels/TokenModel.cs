using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Library.Application.Dtos.AuthModels
{
    public class TokenModel
    {
        public string? Token { get; set; }
        [JsonIgnore, ValidateNever]
        public string Message { get; set; }
    }
}
