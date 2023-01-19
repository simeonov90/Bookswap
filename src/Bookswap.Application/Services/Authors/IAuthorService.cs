using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Infrastructure.Extensions.Models;

namespace Bookswap.Application.Services.Authors
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<PagingPagedResult<AuthorDto>> GetAllAsync(PagingQueryParameters queryParameters);
        Task<AuthorDto> GetById(int id);
        Task<AuthorDto> CreateAsync(CreateAuthorDto createAuthorDto);
        Task UpdateAsync(UpdateAuthorDto updateAuthorDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<AuthorDto>> GetByKeyword(string keyword);
        Task<bool> Exists(int id);
    }
}
