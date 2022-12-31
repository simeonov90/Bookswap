namespace Bookswap.Infrastructure.Repository.IRepository
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAllQueryable();
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        Task Add(TEntity entity);
        Task Delete(TKey id);
        Task Update(TEntity entity);
    }
}
