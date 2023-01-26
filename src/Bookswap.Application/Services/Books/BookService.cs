using Bookswap.Application.Services.Books.Dtos;
using Bookswap.Infrastructure.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Application.Services.Books
{
    public class BookService : IBookService
    {
        public Task<BookDto> CreateAsync(CreateBookDto createAuthorDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagingPagedResult<BookDto>> GetAllAsync(PagingQueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateBookDto updateAuthorDto)
        {
            throw new NotImplementedException();
        }
    }
}
