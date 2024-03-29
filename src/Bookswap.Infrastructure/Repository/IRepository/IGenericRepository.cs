﻿using Bookswap.Infrastructure.Extensions.Models;
using System.Linq.Expressions;

namespace Bookswap.Infrastructure.Repository.IRepository
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAllQueryable();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<PagingPagedResult<TResult>> GetAllAsync<TResult>(PagingQueryParameters queryParameters);
        Task<TEntity> GetById(TKey id);
        Task Add(TEntity entity);
        Task Delete(TKey id, bool softdelete = true);
        Task Update(TEntity entity);
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
    }
}
