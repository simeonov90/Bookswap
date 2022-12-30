﻿using Bookswap.Domain.DbContext;
using Bookswap.Domain.Models;
using Bookswap.Infrastructure.Repository.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Infrastructure.Repository
{
    public class AuthorRepository : GenericRepository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(BookswapDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
