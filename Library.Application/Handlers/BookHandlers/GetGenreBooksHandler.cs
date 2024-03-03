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
    public class GetGenreBooksHandler : IRequestHandler<GetGenreBooksQuery, IEnumerable<GetBooksDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetGenreBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetBooksDto>> Handle(GetGenreBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.Books.GetGenreBooksAsync(request.GenreId);
            return _mapper.Map<IEnumerable<GetBooksDto>>(books);
        }

    }
}
