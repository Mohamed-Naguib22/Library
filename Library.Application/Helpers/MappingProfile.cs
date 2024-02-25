using AutoMapper;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Dtos.BookDtos;
using Library.Application.Dtos.GenreDto;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Helpers
{
    public class MappingProfile : Profile
    {
        private readonly string _baseUrl = "https://localhost:7047/";
        public MappingProfile()
        {
            CreateMap<Author, GetAuthorDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl));
            CreateMap<AddAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Genre, GetGenreDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl));
            CreateMap<AddGenreDto, Genre>();

            CreateMap<Book, GetBookDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.NumOfCopiesInStock > 0));
            CreateMap<Book, GetAllBooksDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<AddBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
