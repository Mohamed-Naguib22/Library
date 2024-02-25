using Library.Application.Dtos.AuthorDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.AuthorQueries
{
    public class GetAuthorQuery : IRequest<GetAuthorDto>
    {
        public int AuthorId { get; }
        public GetAuthorQuery(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
