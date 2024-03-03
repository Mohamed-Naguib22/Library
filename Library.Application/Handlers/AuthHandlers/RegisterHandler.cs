using AutoMapper;
using Library.Application.Commands.AuthCommands;
using Library.Application.Dtos.AuthDtos;
using Library.Application.Interfaces;
using Library.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthHandlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AuthDto>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IJwtService _jwtService;
        private readonly IEmailSender _emailSender;
        private readonly ICacheService _cacheService;
        private readonly TimeSpan _CodeExpiration = TimeSpan.FromMinutes(15);
        public RegisterHandler(IMapper mapper, IIdentityService identityService,
            IEmailSender emailSender, ICacheService cacheService, IJwtService jwtService)
        {
            _mapper = mapper;
            _identityService = identityService;
            _emailSender = emailSender;
            _cacheService = cacheService;
            _jwtService = jwtService;
        }
        public async Task<AuthDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = request.RegisterDto;

            if (await _identityService.GetUserByEmailAsync(registerDto.Email) != null)
                return new AuthDto { Succeeded = false, Message = "This email is already registered" };

            var user = _mapper.Map<ApplicationUser>(registerDto);

            var result = await _identityService.CreateUserAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(r => r.Description).ToList();
                string errorMessage = string.Join(", ", errors);
                return new AuthDto { Succeeded = false, Message = errorMessage };
            }

            await _identityService.AddUserToRoleAsync(user, "User");
            await _identityService.UpdateUserAsync(user);
            //string verificationCode = GenerateRandomCode();

            //await _emailSender.SendEmailAsync(user.Email, "Verification Code", $"Your verification code is {verificationCode}");

            //_cacheService.Set($"{user.Id}_VerificationCode", verificationCode, _CodeExpiration);

            //return new AuthDto { Succeeded = true };
            user.EmailConfirmed = true;

            var jwtSecurityToken = await _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshTokens?.Add(refreshToken);
            user.Cart = new Cart { ApplicationUserId = user.Id , CreatedAt = DateTime.Now};
            user.Wishlist = new Wishlist { ApplicationUserId = user.Id , CreatedAt = DateTime.Now};

            await _identityService.UpdateUserAsync(user);

            return new AuthDto(user, jwtSecurityToken, refreshToken, new List<string> { "User" });
        }

        private static string GenerateRandomCode()
        {
            var random = new Random();
            string code = random.Next(100000, 999999).ToString();
            return code;
        }
    }
}
