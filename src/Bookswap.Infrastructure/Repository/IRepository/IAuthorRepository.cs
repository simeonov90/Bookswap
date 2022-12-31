using Bookswap.Domain.Models;

namespace Bookswap.Infrastructure.Repository.IRepository
{
    public interface IAuthorRepository : IGenericRepository<Author, int>
    {
    }
}
