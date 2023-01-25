using Bookswap.Domain.Models;

namespace Bookswap.Infrastructure.Repository.IRepository
{
    public interface IBookRepository : IGenericRepository<Book, int>
    {
    }
}
