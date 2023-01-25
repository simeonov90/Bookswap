using Bookswap.Application.Services.Books.Dtos;
using Bookswap.Application.Services.Shared;

namespace Bookswap.Application.Services.Books
{
    public interface IBookService : IBaseService<BookDto, CreateBookDto, UpdateBookDto, int>
    {
    }
}
