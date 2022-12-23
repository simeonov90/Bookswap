using Bookswap.Domain.Models;
using Bookswap.Infrastructure.UOW.IUOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Application.Services.Authors
{
    public class AuthorService
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await unitOfWork.Authors.GetAll();
        }
    }
}
