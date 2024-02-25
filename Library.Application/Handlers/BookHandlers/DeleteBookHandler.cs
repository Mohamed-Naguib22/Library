using Library.Application.Commands.AuthorCommands;
using Library.Application.Commands.BookCommands;
using Library.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.BookHandlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public DeleteBookHandler(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }
        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByAsync(b => b.Id == request.BookId);

            if (book == null)
                return false;

            _unitOfWork.Books.Delete(book);
            _imageService.DeleteImage(book.ImgUrl);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
