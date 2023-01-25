using Bookswap.Domain.Models;

namespace Bookswap.Infrastructure.Repository.IRepository
{
    public interface ICoverRepository : IGenericRepository<Cover, Guid>
    {
    }
}
