using AutoMapper;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Domain.Models;
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
            var entites = await unitOfWork.Authors.GetAll();
            return mapper.Map<IEnumerable<AuthorDto>>(entites);
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto createAuthorDto)
        {
            if (string.IsNullOrWhiteSpace(createAuthorDto.FullName)) throw new ArgumentException("Author name can`t be null or whitespace.");

            var entity = mapper.Map<Author>(createAuthorDto);

            await unitOfWork.Authors.Add(entity);
            await unitOfWork.CompletedAsync();

            return mapper.Map<AuthorDto>(entity);
        }

        public async Task<AuthorDto> GetById(int id)
        {
            var entity = await unitOfWork.Authors.GetById(id);

            return mapper.Map<AuthorDto>(entity);
        }

        public async Task UpdateAsync(UpdateAuthorDto updateAuthorDto)
        {
            await unitOfWork.Authors.Update(mapper.Map<Author>(updateAuthorDto));
            await unitOfWork.CompletedAsync();
        }

        public async Task<IEnumerable<AuthorDto>> GetByKeyword(string keyword)
        {
            return mapper.Map<List<AuthorDto>>(await unitOfWork.Authors.GetAllQueryable()
                .Where(a => a.FullName.Contains(keyword))
                .AsNoTracking()
                .ToListAsync());
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.Authors.Delete(id, false);
            await unitOfWork.CompletedAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await unitOfWork.Authors.Exists(a => a.Id == id);
        }
    }
}
