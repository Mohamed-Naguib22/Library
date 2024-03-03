using Library.Application.Commands.AuthCommands;
using Library.Application.Dtos.AuthDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthQueries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthHandlers
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, AuthDto>
    {
        private readonly IJwtService _jwtService;
        private readonly IIdentityService _identityService;
        public ChangePasswordHandler(IJwtService jwtService, IIdentityService identityService)
        {
            _jwtService = jwtService;
            _identityService = identityService;
        }
        public async Task<AuthDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = request.ChangePasswordDto;

            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);
            
            if (user == null)
                return new AuthDto { Succeeded = false, Message = "User is not found" };

            if (!await _identityService.CheckPasswordAsync(user, changePasswordDto.CurrentPassword))
                return new AuthDto { Succeeded = false, Message = "Password is incorrect" };

            var result = await _identityService
                .ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(r => r.Description).ToList();
                string errorMessage = string.Join(", ", errors);
                return new AuthDto { Succeeded = false, Message = errorMessage };
            }

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
