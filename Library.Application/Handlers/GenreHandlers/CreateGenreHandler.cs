using AutoMapper;
using Library.Application.Commands.AuthorCommands;
using Library.Application.Commands.GenreCommands;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Dtos.GenreDto;
using Library.Application.Interfaces;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.GenreHandlers
{
    public class CreateGenreHandler : IRequestHandler<CreateGenreCommand, GetGenreDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public CreateGenreHandler(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<GetGenreDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Genres.ExistsAsync(a => a.Name == request.GenreDto.Name))
                return new GetGenreDto { Succeeded = false, Message = "This name is already added" };

            var genre = _mapper.Map<Genre>(request.GenreDto);
            genre.ImgUrl = _imageService.SetImage(request.GenreDto.Image);

            await _unitOfWork.Genres.AddAsync(genre);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetGenreDto>(genre);
        }
    }
}
