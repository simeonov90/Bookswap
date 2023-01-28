using Bookswap.Infrastructure.Repository.IRepository;

namespace Bookswap.Infrastructure.UOW.IUOW
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Author { get; }
        IGenreRepository Genre { get; }
        ICoverRepository Cover { get; }
        IBookRepository Book { get; }
        Task CompletedAsync();
    }
}
