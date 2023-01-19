using AutoMapper;
using Bookswap.Domain.DbContext;
using Bookswap.Infrastructure.Repository;
using Bookswap.Infrastructure.Repository.IRepository;
using Bookswap.Infrastructure.UOW.IUOW;

namespace Bookswap.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookswapDbContext dbContext;

        public IAuthorRepository Author { get; private set; }

        public IGenreRepository Genre  { get; private set; }


        public UnitOfWork(BookswapDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            Author = new AuthorRepository(dbContext, mapper);
            Genre = new GenreRepository(dbContext, mapper);
        }

        public async Task CompletedAsync() => await dbContext.SaveChangesAsync();

        public void Dispose() => dbContext.Dispose();
    }
}
