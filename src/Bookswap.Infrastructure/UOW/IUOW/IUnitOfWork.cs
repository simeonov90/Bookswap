using Bookswap.Infrastructure.Repository.IRepository;

namespace Bookswap.Infrastructure.UOW.IUOW
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        Task CompletedAsync();
    }
}
