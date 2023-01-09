using AutoMapper;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Accounts.Dto;
using Bookswap.Domain.Models;

namespace Bookswap.Application.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<Author, AuthorDto>();

            CreateMap<UserDto,  BookswapUser>();
        }
    }
}
