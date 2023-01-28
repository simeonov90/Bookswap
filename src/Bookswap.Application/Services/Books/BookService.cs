using AutoMapper;
using Bookswap.Application.Services.Authors.Dto;
using Bookswap.Application.Services.Books.Dtos;
using Bookswap.Domain.DbContext;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Extensions.Models;
using Bookswap.Infrastructure.Repository.IRepository;
using Bookswap.Infrastructure.UOW.IUOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Application.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<BookService> logger;
        private readonly IAuthorRepository authorRepository;
        private readonly BookswapDbContext bookswapDbContext;

        public BookService
            (
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<BookService> logger,
            IAuthorRepository authorRepository,
            BookswapDbContext bookswapDbContext
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
            this.authorRepository = authorRepository;
            this.bookswapDbContext = bookswapDbContext;
        }

        public async Task<BookDto> CreateAsync(CreateBookDto createBookDto)
        {
            if (await authorRepository.Exists(a => a.FullName.Contains(createBookDto.AuthorName)))
            {
                createBookDto.AuthorId = await authorRepository.GetAllQueryable()
                    .Where(a => a.FullName.Contains(createBookDto.AuthorName))
                    .AsNoTracking()
                    .Select(a => a.Id)
                    .FirstOrDefaultAsync();
            }
            else
            {
                var authorEntity = new Author()
                {
                    FullName = createBookDto.AuthorName
                };

                await authorRepository.Add(authorEntity);
                await unitOfWork.CompletedAsync();

                createBookDto.AuthorId = authorEntity.Id;
            }
            
            var entity = mapper.Map<Book>(createBookDto);
            await unitOfWork.Book.Add(entity);
            await unitOfWork.CompletedAsync();

            return mapper.Map<BookDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.Book.Delete(id);
            await unitOfWork.CompletedAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await unitOfWork.Book.Exists(b => b.Id == id);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<BookDto>>
                (
                    await unitOfWork.Book.GetAllQueryable()
                    .Where(b => !b.IsDeleted)
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .AsNoTracking()
                    .ToListAsync()
                );
        }

        public async Task<PagingPagedResult<BookDto>> GetAllAsync(PagingQueryParameters queryParameters)
        {
            return await unitOfWork.Book.GetAllAsync<BookDto>(queryParameters);
        }

        public async Task<BookDto> GetById(int id)
        {
            var dbTest = await bookswapDbContext.Books.FindAsync(id);
            var test = await unitOfWork.Book.GetById(id);
            return mapper.Map<BookDto>(await unitOfWork.Book.GetById(id));
        }

        public async Task UpdateAsync(UpdateBookDto updateBookDto)
        {
            await unitOfWork.Book.Update(mapper.Map<Book>(updateBookDto));
            await unitOfWork.CompletedAsync();
        }
    }
}
