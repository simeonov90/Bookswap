using AutoMapper;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Genres.Dto;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.UOW.IUOW;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<GenreDto>>(await unitOfWork.Genre.GetAllAsync());
        }

        public Task<GenreDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GenreDto>> GetByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateGenreDto updateAuthorDto)
        {
            throw new NotImplementedException();
        }
    }
}
