using Library.Application.Commands.AuthCommands;
using Library.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthHandlers
{
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, bool>
    {
        private readonly IJwtService _jwtService;
        private readonly IIdentityService _identityService;
        public RevokeTokenHandler(IJwtService jwtService, IIdentityService identityService)
        {
            _jwtService = jwtService;
            _identityService = identityService;
        }
        public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.Token);

            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == request.Token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _identityService.UpdateUserAsync(user);

            return true;
        }
    }
}
