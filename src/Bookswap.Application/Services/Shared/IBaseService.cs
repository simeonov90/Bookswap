using Bookswap.Infrastructure.Extensions.Models;

namespace Bookswap.Application.Services.Shared
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto, TKey>
    {
        Task<IEnumerable<TEntityDto>> GetAllAsync();
        Task<PagingPagedResult<TEntityDto>> GetAllAsync(PagingQueryParameters queryParameters);
        Task<TEntityDto> GetById(TKey id);
        Task<TEntityDto> CreateAsync(TEntityCreateDto createAuthorDto);
        Task UpdateAsync(TEntityUpdateDto updateAuthorDto);
        Task DeleteAsync(TKey id);
        Task<bool> Exists(TKey id);
    }
}
