using AutoMapper;
using Bookswap.Domain.DbContext;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Infrastructure.Repository
{
    public class GenreRepository : GenericRepository<Genre, int>, IGenreRepository
    {
        public GenreRepository(BookswapDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
