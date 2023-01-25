using AutoMapper;
using Bookswap.Application.Services.Genres.Dto;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Extensions.Models;
using Bookswap.Infrastructure.UOW.IUOW;
using Microsoft.Extensions.Logging;

namespace Bookswap.Application.Services.Genres
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<GenreService> logger;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GenreService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<GenreDto> CreateAsync(CreateGenreDto createGenreDto)
        {
            if (string.IsNullOrWhiteSpace(createGenreDto.Name)) throw new ArgumentException("Genre name can`t be null or whitespace.");

            var entity = mapper.Map<Genre>(createGenreDto);

            await unitOfWork.Genre.Add(entity);
            await unitOfWork.CompletedAsync();

            return mapper.Map<GenreDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.Genre.Delete(id, false);
            await unitOfWork.CompletedAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await unitOfWork.Genre.Exists(e => e.Id == id);
        }

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<GenreDto>>(await unitOfWork.Genre.GetAllAsync());
        }

        public async Task<PagingPagedResult<GenreDto>> GetAllAsync(PagingQueryParameters queryParameters)
        {
            return await unitOfWork.Genre.GetAllAsync<GenreDto>(queryParameters);
        }

        public async Task<GenreDto> GetById(int id)
        {
            return mapper.Map<GenreDto>(await unitOfWork.Genre.GetById(id));
        }

        public async Task UpdateAsync(UpdateGenreDto updateAuthorDto)
        {
            await unitOfWork.Genre.Update(mapper.Map<Genre>(updateAuthorDto));
            await unitOfWork.CompletedAsync();
        }
    }
}
