using Bookswap.Infrastructure.Repository.IRepository;

namespace Bookswap.Infrastructure.UOW.IUOW
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Author { get; }
        IGenreRepository Genre { get; }
        Task CompletedAsync();
    }
}
