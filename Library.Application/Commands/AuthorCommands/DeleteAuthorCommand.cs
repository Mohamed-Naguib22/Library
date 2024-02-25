using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.AuthorCommands
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int AuthotId { get; }

        public DeleteAuthorCommand(int authorId)
        {
            AuthotId = authorId;
        }
    }
}
