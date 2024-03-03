using Library.Application.Dtos.AuthDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.AuthCommands
{
    public class RefreshTokenCommand : IRequest<AuthDto>
    {
        public string Token { get; }
        public RefreshTokenCommand(string token) 
        {
            Token = token;
        }
    }
}
