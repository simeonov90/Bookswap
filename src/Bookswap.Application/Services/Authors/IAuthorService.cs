using Bookswap.Application.Services.Authors.Dto;

namespace Bookswap.Application.Services.Authors
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetById(int id);
        Task<AuthorDto> CreateAsync(CreateAuthorDto createAuthorDto);
        Task UpdateAsync(UpdateAuthorDto updateAuthorDto);
        Task<IEnumerable<AuthorDto>> GetByKeyword(string keyword);
    }
}
