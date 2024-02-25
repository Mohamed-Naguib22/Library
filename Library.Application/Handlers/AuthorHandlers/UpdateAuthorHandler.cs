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
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, GetAuthorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public UpdateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<GetAuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByAsync(a => a.Id == request.AuthorId);

            if (author == null)
                return new GetAuthorDto { Succeeded = false, Message = "Author not found" };

            if (await _unitOfWork.Authors.ExistsAsync(a => a.Name == request.AuthorDto.Name))
                return new GetAuthorDto { Succeeded = false, Message = "This name is already added" };

            author.Name = request.AuthorDto.Name ?? author.Name;
            author.Biography = request.AuthorDto.Biography ?? author.Biography;

            if (request.AuthorDto.Image != null)
                author.ImgUrl = _imageService.SetImage(request.AuthorDto.Image, author.ImgUrl);

            _unitOfWork.Authors.Update(author);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetAuthorDto>(author);
        }
    }
}
