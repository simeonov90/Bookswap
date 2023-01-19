using Bookswap.Application.Services.Genres.Dto;

namespace Bookswap.Application.Services.Genres
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();
        Task<GenreDto> GetById(int id);
        Task<GenreDto> CreateAsync(CreateGenreDto createAuthorDto);
        Task UpdateAsync(UpdateGenreDto updateAuthorDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GenreDto>> GetByKeyword(string keyword);
        Task<bool> Exists(int id);
    }
}
