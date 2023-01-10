using Bookswap.Domain.DbContext;
using Bookswap.Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Infrastructure.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private readonly BookswapDbContext dbContext;

        public GenericRepository(BookswapDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Add(TEntity entity)
        {
            await dbContext.AddAsync(entity);
        }

        public async Task Delete(TKey id)
        {
            var entity = await GetById(id);
            if (entity is null) throw new ArgumentException($"There is no entity with id={id}");
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
    }
}
