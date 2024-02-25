using AutoMapper;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Interfaces;
using Library.Application.Queries.AuthorQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthorHandlers
{
    public class GetAuthorHandler : IRequestHandler<GetAuthorQuery, GetAuthorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetAuthorDto> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByAsync(a => a.Id == request.AuthorId);

            if (author == null)
                return new GetAuthorDto { Succeeded = false, Message = "Author not found" };

            return _mapper.Map<GetAuthorDto>(author);
        }
    }
}
