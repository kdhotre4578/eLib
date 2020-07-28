using eLib.Core.Models;

namespace eLib.Core.Contracts
{
    public interface IUoW
    {
        IRepository<Book> BookRepository { get; }

        IRepository<BookBorrower> BookBorrowerRepository { get; }

        void Commit();
    }
}
