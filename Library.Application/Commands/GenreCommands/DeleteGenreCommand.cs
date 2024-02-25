using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.GenreCommands
{
    public class DeleteGenreCommand : IRequest<bool>
    {
        public int GenreId { get; set; }
        public DeleteGenreCommand(int genreId) 
        {
            GenreId = genreId;
        }
    }
}
