using AutoMapper;
using Library.Application.Commands.AuthCommands;
using Library.Application.Dtos.AuthDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthQueries;
using Library.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthHandlers
{
    public class VerifyAccountHandler : IRequestHandler<VerifyAccountQuery, AuthDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtService _jwtService;
        private readonly ICacheService _cacheService;
        public VerifyAccountHandler(IIdentityService identityService,
            IJwtService jwtService, ICacheService cacheService)
        {
            _identityService = identityService;
            _jwtService = jwtService;
            _cacheService = cacheService;
        }
        public async Task<AuthDto> Handle(VerifyAccountQuery request, CancellationToken cancellationToken)
        {
            var verifyAccountDto = request.VerifyAccountDto;
            var user = await _identityService.GetUserByEmailAsync(verifyAccountDto.Email);

            if (user == null)
                return new AuthDto { Succeeded = false, Message = "The email is incorrect" };

            var cachedCode = _cacheService.Get<string>($"{user.Id}_VerificationCode");

            if (cachedCode == null || verifyAccountDto.VerificationCode != cachedCode)
                return new AuthDto { Succeeded = false, Message = "The verification code is invalid or expired" };

            user.EmailConfirmed = true;

            var jwtSecurityToken = await _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshTokens?.Add(refreshToken);

            await _identityService.UpdateUserAsync(user);

            return new AuthDto(user, jwtSecurityToken, refreshToken, new List<string> { "User" });
        }
    }
}
