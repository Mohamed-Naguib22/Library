using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IJwtService
    {
        Task<JwtSecurityToken> GenerateAccessToken(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
        Task<ApplicationUser?> GetUserByRefreshToken(string refreshToken);
    }
}
