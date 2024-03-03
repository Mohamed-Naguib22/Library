using AutoMapper;
using Library.Application.Dtos.BookDtos;
using Library.Application.Dtos.GenreDto;
using Library.Application.Interfaces;
using Library.Application.Queries.BookQueries;
using Library.Application.Queries.GenreQueries;
using Library.Domain.Const;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.BookHandlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<GetBooksDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllBooksHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetBooksDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.Books.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<GetBooksDto>>(books);
        }
    }
}
