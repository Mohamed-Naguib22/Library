using Library.Application.Dtos.AuthorDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.AuthorQueries
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<GetAuthorDto>>
    {
    }
}
