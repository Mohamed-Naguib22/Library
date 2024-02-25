using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.BookCommands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
        public DeleteBookCommand(int bookId) 
        {
            BookId = bookId;
        }
    }
}
