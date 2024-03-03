using Library.Application.Dtos.AuthDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.AuthCommands
{
    public class ChangePasswordCommand : IRequest<AuthDto>
    {
        public ChangePasswordDto ChangePasswordDto { get; }
        public string RefreshToken { get; }
        public ChangePasswordCommand(ChangePasswordDto changePasswordDto, string refreshToken) 
        {
            ChangePasswordDto = changePasswordDto;
            RefreshToken = refreshToken;
        }
    }
}
