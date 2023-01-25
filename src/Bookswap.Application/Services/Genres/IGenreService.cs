using Bookswap.Application.Services.Genres.Dto;
using Bookswap.Application.Services.Shared;

namespace Bookswap.Application.Services.Genres
{
    public interface IGenreService : IBaseService<GenreDto, CreateGenreDto, UpdateGenreDto, int>
    {

    }
}
