using AutoMapper;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Dtos.BookDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthorQueries;
using Library.Application.Queries.BookQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.BookHandlers
{
    public class GetBookHandler : IRequestHandler<GetBookQuery, GetBookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetBookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetBookById(request.BookId);

            if (book == null)
                return new GetBookDto { Succeeded = false, Message = "Book is not found" };

            var bookDto = _mapper.Map<GetBookDto>(book);
            bookDto.Author = book.Author.Name;
            bookDto.BookGenres = book.BookGenres.Select(bg => bg.Genre.Name);
            
            return bookDto;
        }
    }
}
