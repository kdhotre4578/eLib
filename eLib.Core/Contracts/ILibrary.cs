using eLib.Core.Models;
using System.Collections.Generic;

namespace eLib.Core.Contracts
{
    public interface ILibrary
    {
        public IEnumerable<Book> GetBooksList();

        public IEnumerable<BookBorrower> GetBorrowedBookList(int userId);

        void BorrowBook(int bookId, int userId);

        void SubmitBook(int bookId, int userId);

        bool CanBorrow(int userId);

        bool IsAlreadyBorrowed(int userId, int bookId);
    }
}
