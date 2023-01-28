using AutoMapper;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Accounts.Dto;
using Bookswap.Domain.Models;
using Bookswap.Application.Services.Genres.Dto;
using Bookswap.Application.Services.Covers.Dto;
using Bookswap.Application.Services.Books.Dtos;

namespace Bookswap.Application.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<Author, AuthorDto>();

            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();
            CreateMap<Genre, GenreDto>();

            CreateMap<UserDto,  BookswapUser>();

            CreateMap<CreateCoverDto, Cover>();
            CreateMap<UpdateCoverDto, Cover>();
            CreateMap<Cover, CoverDto>();

            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Book, BookDto>();
        }
    }
}
