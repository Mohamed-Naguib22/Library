using AutoMapper;
using Library.Application.Commands.BookCommands;
using Library.Application.Dtos.BookDtos;
using Library.Application.Interfaces;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.BookHandlers
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, GetBookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public UpdateBookHandler(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<GetBookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByAsync(a => a.Id == request.BookId);

            if (await _unitOfWork.Books.ExistsAsync(b => b.Title == request.BookDto.Title))
                return new GetBookDto { Succeeded = false, Message = "This book is already added" };

            if (await _unitOfWork.Books.ExistsAsync(b => b.ISBN == request.BookDto.ISBN))
                return new GetBookDto { Succeeded = false, Message = "This book is already added" };

            book.Title = request.BookDto.Title ?? book.Title;
            book.ISBN = request.BookDto.ISBN ?? book.ISBN;
            book.Publisher = request.BookDto.Publisher ?? book.Publisher;
            book.Summary = request.BookDto.Summary ?? book.Summary;
            book.Language = request.BookDto.Language ?? book.Language;
            book.Price = request.BookDto.Price ?? book.Price;
            book.NumOfCopiesInStock = request.BookDto.NumOfCopiesInStock ?? book.NumOfCopiesInStock;
            book.NumOfPages = request.BookDto.NumOfPages ?? book.NumOfPages;
            book.PublicationDate = request.BookDto.PublicationDate ?? book.PublicationDate;
            book.AuthorId = request.BookDto.AuthorId ?? book.AuthorId;

            if (request.BookDto.Image != null)
                book.ImgUrl = _imageService.SetImage(request.BookDto.Image, book.ImgUrl);

            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetBookDto>(book);
        }
    }
}
