using Library.Application.Commands.AuthorCommands;
using Library.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthorHandlers
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public DeleteAuthorHandler(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }
        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByAsync(a => a.Id == request.AuthotId);

            if (author == null)
                return false;

            _unitOfWork.Authors.Delete(author);
            _imageService.DeleteImage(author.ImgUrl);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
