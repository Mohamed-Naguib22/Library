using AutoMapper;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Dtos.GenreDto;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthorQueries;
using Library.Application.Queries.GenreQueries;
using Library.Domain.Const;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.GenreHandlers
{
    public class GetAllGenresHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GetGenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllGenresHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetGenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _unitOfWork.Genres.GetAllAsync(null, a => a.Name, OrderBy.Ascending);
            return _mapper.Map<IEnumerable<GetGenreDto>>(genres);
        }
    }
}
