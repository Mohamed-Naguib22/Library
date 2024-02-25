using AutoMapper;
using Library.Application.Commands.AuthorCommands;
using Library.Application.Commands.BookCommands;
using Library.Application.Dtos.AuthorDtos;
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
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, GetBookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public CreateBookHandler(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<GetBookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Books.ExistsAsync(b => b.Title == request.BookDto.Title))
                return new GetBookDto { Succeeded = false, Message = "This book is already added" };

            if (await _unitOfWork.Books.ExistsAsync(b => b.ISBN == request.BookDto.ISBN))
                return new GetBookDto { Succeeded = false, Message = "This book is already added" };

            var book = _mapper.Map<Book>(request.BookDto);
            book.BookGenres = request.BookDto.BookGenreId.Select(ids => new BookGenre { GenreId = ids }).ToList();
            book.ImgUrl = _imageService.SetImage(request.BookDto.Image);
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetBookDto>(book);
        }
    }
}
