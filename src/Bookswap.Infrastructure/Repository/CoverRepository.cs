using AutoMapper;
using Bookswap.Domain.DbContext;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Repository.IRepository;

namespace Bookswap.Infrastructure.Repository
{
    public class CoverRepository : GenericRepository<Cover, Guid>, ICoverRepository
    {
        public CoverRepository(BookswapDbContext dbContext, IMapper mapper)
            :base(dbContext, mapper)
        {
            
        }
    }
}
