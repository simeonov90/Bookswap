using AutoMapper;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Extensions.Models;
using Bookswap.Infrastructure.UOW.IUOW;

namespace Bookswap.Application.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
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
            return mapper.Map<AuthorDto>(await unitOfWork.Author.GetById(id));
        }

        public async Task UpdateAsync(UpdateAuthorDto updateAuthorDto)
        {
            await unitOfWork.Author.Update(mapper.Map<Author>(updateAuthorDto));
            await unitOfWork.CompletedAsync();
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
