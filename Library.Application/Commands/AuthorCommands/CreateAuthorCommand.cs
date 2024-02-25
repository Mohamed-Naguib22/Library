using Library.Application.Dtos.AuthorDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.AuthorCommands
{
    public class CreateAuthorCommand : IRequest<GetAuthorDto>
    {
        public AddAuthorDto AuthorDto { get; }

        public CreateAuthorCommand(AddAuthorDto authorDto)
        {
            AuthorDto = authorDto;
        }
    }
}
