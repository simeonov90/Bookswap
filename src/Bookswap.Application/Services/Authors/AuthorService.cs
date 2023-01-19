using AutoMapper;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Extensions.Models;
using Bookswap.Infrastructure.UOW.IUOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Application.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorService> logger;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuthorService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<AuthorDto>>(await unitOfWork.Author.GetAllAsync());
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto createAuthorDto)
        {
            if (string.IsNullOrWhiteSpace(createAuthorDto.FullName)) throw new ArgumentException("Author name can`t be null or whitespace.");

            var entity = mapper.Map<Author>(createAuthorDto);

            await unitOfWork.Author.Add(entity);
            await unitOfWork.CompletedAsync();

            return mapper.Map<AuthorDto>(entity);
        }

        public async Task<AuthorDto> GetById(int id)
        {
            var entity = await unitOfWork.Author.GetById(id);

            return mapper.Map<AuthorDto>(entity);
        }

        public async Task UpdateAsync(UpdateAuthorDto updateAuthorDto)
        {
            await unitOfWork.Author.Update(mapper.Map<Author>(updateAuthorDto));
            await unitOfWork.CompletedAsync();
        }

        public async Task<IEnumerable<AuthorDto>> GetByKeyword(string keyword)
        {
            return mapper.Map<List<AuthorDto>>(await unitOfWork.Author.GetAllQueryable()
                .Where(a => a.FullName.Contains(keyword))
                .AsNoTracking()
                .ToListAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.Author.Delete(id, false);
            await unitOfWork.CompletedAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await unitOfWork.Author.Exists(a => a.Id == id);
        }

        public async Task<PagingPagedResult<AuthorDto>> GetAllAsync(PagingQueryParameters queryParameters)
        {
            return await unitOfWork.Author.GetAllAsync<AuthorDto>(queryParameters);
        }
    }
}
