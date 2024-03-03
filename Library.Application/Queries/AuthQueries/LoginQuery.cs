using Library.Application.Dtos.AuthDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.AuthQueries
{
    public class LoginQuery : IRequest<AuthDto>
    {
        public LoginDto LoginDto { get; }
        public LoginQuery(LoginDto loginDto) 
        {
            LoginDto = loginDto;
        }
    }
}
