using AutoMapper;
using Library.Application.Dtos.BookDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.BookQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.BookHandlers
{
    public class GetBookHandler : IRequestHandler<GetBookQuery, ReturnBookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReturnBookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetBookById(request.BookId);

            if (book == null)
                return new ReturnBookDto { Succeeded = false, Message = "Book is not found" };

            var bookDto = _mapper.Map<ReturnBookDto>(book);
            bookDto.Author = book.Author.Name;
            bookDto.BookGenres = book.BookGenres.Select(bg => bg.Genre.Name);
            
            if (book.Ratings.Any())
                bookDto.Rating = book.Ratings.Average(r => r.Rate);

            return bookDto;
        }
    }
}
