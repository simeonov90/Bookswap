using System.Linq.Expressions;

namespace Bookswap.Infrastructure.Repository.IRepository
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAllQueryable();
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        Task Add(TEntity entity);
        Task Delete(TKey id, bool softdelete = true);
        Task Update(TEntity entity);
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
    }
}
