using Library.Application.Dtos.AuthDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.AuthCommands
{
    public class RegisterCommand : IRequest<AuthDto>
    {
        public RegisterDto RegisterDto { get; }
        public RegisterCommand(RegisterDto registerDto) 
        {
            RegisterDto = registerDto;
        }
    }
}
