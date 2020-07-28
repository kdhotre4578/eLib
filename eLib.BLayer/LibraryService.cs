using eLib.Core.Contracts;
using eLib.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace eLib.BLayer
{
    public class LibraryService : ILibrary
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookBorrower> _bookBorrowRepository;
        private readonly IUoW _uow;

        public LibraryService(IUoW uow)
        {
            _bookRepository = uow.BookRepository;
            _bookBorrowRepository = uow.BookBorrowerRepository;
            _uow = uow;
        }

        public IEnumerable<Book> GetBooksList()
        {
            return _bookRepository.Get();
        }

        public IEnumerable<BookBorrower> GetBorrowedBookList(int userId)
        {
            return _bookBorrowRepository.Get(x => x.UserId == userId);
        }

        public void BorrowBook(int bookId, int userId)
        {
            var book = _bookRepository.Get(x => x.Id == bookId).FirstOrDefault();

            if (book != null)
            {
                if (book.Quantity > 0)
                {
                    --book.Quantity;
                    _bookRepository.Update(book);

                    _bookBorrowRepository.Add(new BookBorrower() { BookId = bookId, UserId = userId });

                    _uow.Commit();
                }
            }
        }

        public void SubmitBook(int bookId, int userId)
        {
            var book = _bookRepository.Get(x => x.Id == bookId).FirstOrDefault();
            ++book.Quantity;
            _bookRepository.Update(book);

            var borrowedBook = _bookBorrowRepository.Get(x => x.BookId == bookId).FirstOrDefault();
            _bookBorrowRepository.Delete(borrowedBook);

            _uow.Commit();
        }

        public bool CanBorrow(int userId)
        {
            var borrowedBooks = _bookBorrowRepository.Get(x => x.UserId == userId);
            return borrowedBooks.Count() < 2;
        }

        public bool IsAlreadyBorrowed(int userId, int bookId)
        {
            var books = _bookBorrowRepository.Get(x => x.UserId == userId && x.BookId == bookId);
            return books.Count() > 0;
        }
    }
}
