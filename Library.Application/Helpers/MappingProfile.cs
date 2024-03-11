using AutoMapper;
using Library.Application.Dtos.AuthDtos;
using Library.Application.Dtos.AuthorDtos;
using Library.Application.Dtos.BookDtos;
using Library.Application.Dtos.CartDtos;
using Library.Application.Dtos.GenreDto;
using Library.Application.Dtos.OrderDtos;
using Library.Application.Dtos.WishlistDtos;
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
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => "\\images\\Default_User_Image.png"))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<Author, GetAuthorDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl));
            CreateMap<AddAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Genre, GetGenreDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl));
            CreateMap<AddGenreDto, Genre>();

            CreateMap<Book, ReturnBookDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.NumOfCopiesInStock > 0))
                .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.AddedOn.ToString("MMMM d, yyyy")));
            CreateMap<Book, GetBooksDto>()
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => _baseUrl + src.ImgUrl))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<AddBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();

            CreateMap<Cart, GetCartDto>()
                .ForMember(dest => dest.TotalCost, 
                    opt => opt.MapFrom(src => src.CartItems.Select(ci => ci.Book.Price * ci.Quantity).Sum()))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems.OrderByDescending(ci => ci.AddedOn)));
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Book.Price * src.Quantity))
                .ForMember(dest => dest.AddedOn, opt => opt.MapFrom(src => src.AddedOn.ToString("MMMM d, yyyy")))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.BookImageUrl, opt => opt.MapFrom(src => _baseUrl + src.Book.ImgUrl));

            CreateMap<Wishlist, GetWishlistDto>()
                .ForMember(dest => dest.WishlistItems, opt => opt.MapFrom(src => src.WishlistItems.OrderByDescending(ci => ci.AddedOn)));
            CreateMap<WishlistItem, WishlistItemDto>()
                .ForMember(dest => dest.AddedOn, opt => opt.MapFrom(src => src.AddedOn.ToString("MMMM d, yyyy")))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.BookImageUrl, opt => opt.MapFrom(src => _baseUrl + src.Book.ImgUrl));

            CreateMap<Order, GetOrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PlacedOn, opt => opt.MapFrom(src => src.PlacedOn.ToString("MMMM d, yyyy")));
            CreateMap<OrderItem, GetOrderItemDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.BookImageUrl, opt => opt.MapFrom(src => _baseUrl + src.Book.ImgUrl))
                .ForMember(dest => dest.SubTotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Book.Price));
        }
    }
}
