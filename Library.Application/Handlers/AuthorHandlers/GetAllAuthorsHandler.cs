using AutoMapper;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthorQueries;
using Library.Domain.Const;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthorHandlers
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<GetAuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllAuthorsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _unitOfWork.Authors.GetAllAsync(a => a.Name, OrderBy.Ascending);
            return _mapper.Map<IEnumerable<GetAuthorDto>>(authors);
        }
    }
}
