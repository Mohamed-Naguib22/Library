using Library.Application.Dtos.GenreDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.GenreQueries
{
    public class GetAllGenresQuery : IRequest<IEnumerable<GetGenreDto>>
    {
    }
}
