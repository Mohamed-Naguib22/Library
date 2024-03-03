using Library.Application.Commands.AuthCommands;
using Library.Application.Dtos.AuthDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthQueries;
using Library.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthHandlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthDto>
    {
        private readonly IJwtService _jwtService;
        private readonly IIdentityService _identityService;
        public RefreshTokenHandler(IJwtService jwtService, IIdentityService identityService)
        {
            _jwtService = jwtService;
            _identityService = identityService;
        }
        public async Task<AuthDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.Token);

            if (user == null)
                return new AuthDto { Succeeded = false, Message = "Invalid Token" };

            var refreshToken = user.RefreshTokens.Single(t => t.Token == request.Token);

            if (!refreshToken.IsActive)
                return new AuthDto { Succeeded = false, Message = "Inactive Token" };

            refreshToken.RevokedOn = DateTime.Now;

            var newRefreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            user.Cart = new Cart { ApplicationUserId = user.Id };

            await _identityService.UpdateUserAsync(user);

            var jwtSecurityToken = await _jwtService.GenerateAccessToken(user);
            var roles = await _identityService.GetUserRolesAsync(user);

            return new AuthDto(user, jwtSecurityToken, refreshToken, roles.ToList());
        }
    }
}
