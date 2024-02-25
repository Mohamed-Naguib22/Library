using Library.Application.Dtos.AuthorDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.AuthorCommands
{
    public class UpdateAuthorCommand : IRequest<GetAuthorDto>
    {
        public int AuthorId { get; }
        public UpdateAuthorDto AuthorDto { get; }
        public UpdateAuthorCommand(int authorId, UpdateAuthorDto authorDto)
        {
            AuthorId = authorId;
            AuthorDto = authorDto;
        }
    }
}
