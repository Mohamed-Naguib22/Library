using AutoMapper;
using Library.Application.Commands.AuthorCommands;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Interfaces;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.AuthorHandlers
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, GetAuthorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public CreateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<GetAuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Authors.ExistsAsync(a => a.Name == request.AuthorDto.Name))
                return new GetAuthorDto { Succeeded = false, Message = "This name is already added" };

            var author = _mapper.Map<Author>(request.AuthorDto);
            author.ImgUrl = _imageService.SetImage(request.AuthorDto.Image);

            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetAuthorDto>(author);
        }
    }
}
