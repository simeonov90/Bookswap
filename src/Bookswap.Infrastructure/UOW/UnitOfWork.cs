using Bookswap.Domain.DbContext;
using Bookswap.Infrastructure.Repository;
using Bookswap.Infrastructure.Repository.IRepository;
using Bookswap.Infrastructure.UOW.IUOW;
using Microsoft.Extensions.Logging;

namespace Bookswap.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookswapDbContext dbContext;

        public IAuthorRepository Authors { get; private set; }

        public UnitOfWork(BookswapDbContext dbContext)
        {
            this.dbContext = dbContext;

            Authors = new AuthorRepository(dbContext);
        }

        public async Task CompletedAsync() => await dbContext.SaveChangesAsync();

        public void Dispose() => dbContext.Dispose();
    }
}
