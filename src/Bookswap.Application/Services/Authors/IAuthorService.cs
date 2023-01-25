using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Shared;

namespace Bookswap.Application.Services.Authors
{
    public interface IAuthorService : IBaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto, int>
    {
    }
}
