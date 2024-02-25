using Library.Application.Commands.GenreCommands;
using Library.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.GenreHandlers
{
    public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public DeleteGenreHandler(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }
        public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.Genres.GetByAsync(g => g.Id == request.GenreId);

            if (genre == null)
                return false;

            _unitOfWork.Genres.Delete(genre);
            _imageService.DeleteImage(genre.ImgUrl);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
