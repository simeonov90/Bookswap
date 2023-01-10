using Bookswap.Domain.DbContext;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Repository.IRepository;

namespace Bookswap.Infrastructure.Repository
{
    public class AuthorRepository : GenericRepository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(BookswapDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
