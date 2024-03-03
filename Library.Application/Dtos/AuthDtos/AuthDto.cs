using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Application.Dtos.AuthDtos
{
    public class AuthDto
    {
        public AuthDto()
        {

        }
        public AuthDto(ApplicationUser user, JwtSecurityToken jwtSecurityToken, RefreshToken refreshToken, List<string> roles)
        {
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            IsAuthenticated = true;
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            ExpiresOn = jwtSecurityToken.ValidTo;
            Roles = roles;
            RefreshToken = refreshToken.Token;
            RefreshTokenExpiration = refreshToken.ExpiresOn;
            Succeeded = true;
        }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonIgnore]
        public bool Succeeded { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
