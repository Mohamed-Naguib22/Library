using Library.Application.Dtos.AuthDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthHandlers
{
    public class LoginHandler : IRequestHandler<LoginQuery, AuthDto>
    {
        private readonly IJwtService _jwtService;
        private readonly IIdentityService _identityService;
        public LoginHandler(IJwtService jwtService, IIdentityService identityService)
        {
            _jwtService = jwtService;
            _identityService = identityService;
        }
        public async Task<AuthDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var loginDto = request.LoginDto;

            var user = await _identityService.GetUserByEmailAsync(loginDto.Email);

            if (user == null || !await _identityService.CheckPasswordAsync(user, loginDto.Password) || !user.EmailConfirmed)
                return new AuthDto { Succeeded = false, Message = "Email or password incorrect" };

            var jwtSecurityToken = await _jwtService.GenerateAccessToken(user);
            var roles = await _identityService.GetUserRolesAsync(user);

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                return new AuthDto(user, jwtSecurityToken, activeRefreshToken, roles.ToList());
            }
            else
            {
                var refreshToken = _jwtService.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _identityService.UpdateUserAsync(user);
                return new AuthDto(user, jwtSecurityToken, refreshToken, roles.ToList());
            }
        }
    }
}
