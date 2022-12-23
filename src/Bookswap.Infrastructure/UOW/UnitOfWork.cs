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
        private readonly ILogger logger;

        public IAuthorRepository Authors { get; private set; }

        public UnitOfWork(BookswapDbContext dbContext, ILogger logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;

            Authors = new AuthorRepository(dbContext, logger);
        }

        public async Task<int> CompletedAsync() => await dbContext.SaveChangesAsync();

        public void Dispose() => dbContext.Dispose();
    }
}
