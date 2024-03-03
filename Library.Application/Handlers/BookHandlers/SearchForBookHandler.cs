using AutoMapper;
using Library.Application.Dtos.BookDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.BookQueries;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.BookHandlers
{
    public class SearchForBookHandler : IRequestHandler<SearchForBookQuery, IEnumerable<GetBooksDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public SearchForBookHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        public async Task<IEnumerable<GetBooksDto>> Handle(SearchForBookQuery request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.Books.SearchAsync(request.SearchQuery);

            if (request.RefreshToken != null)
            {
                var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);
                var searchQuery = new SearchQuery { ApplicationUser = user, Query = request.SearchQuery, TimSpan = DateTime.Now };
                await _unitOfWork.SearchQueries.AddAsync(searchQuery);
                await _unitOfWork.CompleteAsync();
            }

            return _mapper.Map<IEnumerable<GetBooksDto>>(books);
        }
    }
}
