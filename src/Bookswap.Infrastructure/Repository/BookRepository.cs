using AutoMapper;
using Bookswap.Domain.DbContext;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Repository.IRepository;

namespace Bookswap.Infrastructure.Repository
{
    public class BookRepository : GenericRepository<Book, int>, IBookRepository
    {
        public BookRepository(BookswapDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
