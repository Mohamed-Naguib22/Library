using Library.Application.Dtos.GenreDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.GenreCommands
{
    public class CreateGenreCommand : IRequest<GetGenreDto>
    {
        public AddGenreDto GenreDto { get;}
        public CreateGenreCommand(AddGenreDto genreDto)
        {
            GenreDto = genreDto;
        }
    }
}
