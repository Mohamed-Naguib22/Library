using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.AuthCommands
{
    public class RevokeTokenCommand : IRequest<bool>
    {
        public string Token { get; }
        public RevokeTokenCommand(string token)
        {
            Token = token;
        }
    }
}
