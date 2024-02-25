using Library.Application.Dtos.BookDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.BookCommands
{
    public class UpdateBookCommand : IRequest<GetBookDto>
    {
        public int BookId { get; }
        public UpdateBookDto BookDto { get; }

        public UpdateBookCommand(int bookId, UpdateBookDto bookDto)
        {
            BookId = bookId;
            BookDto = bookDto;
        }
    }
}
