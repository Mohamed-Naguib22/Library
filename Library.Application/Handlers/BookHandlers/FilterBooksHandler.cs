using AutoMapper;
using Library.Application.Dtos.BookDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.BookQueries;
using Library.Application.Services;
using Library.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.BookHandlers
{
    public class FilterBooksHandler : IRequestHandler<FilterBooksQuery, IEnumerable<GetBooksDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFilterFactory<Book, BookFilterDto> _filterFactory;
        public FilterBooksHandler(IUnitOfWork unitOfWork, IMapper mapper, IFilterFactory<Book, BookFilterDto> filterFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterFactory = filterFactory;
        }

        public async Task<IEnumerable<GetBooksDto>> Handle(FilterBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Books.GetAllBooksQuery();

            var filterStrategies = _filterFactory.CreateFilterStrategies(request.BookFilterDto);

            foreach (var strategy in filterStrategies)
            {
                query = strategy.ApplyFilter(query);
            }

            var filteredBooks = await query.ToListAsync();
            return _mapper.Map<IEnumerable<GetBooksDto>>(filteredBooks);
        }
    }
}
