using eLib.Core.Models;
using eLib.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace eLib.Helper
{
    public class HelperMethods
    {
        public IEnumerable<BorrowDetailsVM> GetBorrowBookDetails(IEnumerable<Book> books, IEnumerable<BookBorrower> bookBorrows)
        {
            List<BorrowDetailsVM> borrowDetails = new List<BorrowDetailsVM>();

            var borrowedBookDetails = (from b in books
                                      join br in bookBorrows on b.Id equals br.BookId
                                      select new { br.Id, br.BookId, b.Name, br.UserId}).ToList();


            foreach (var bookBorrow in borrowedBookDetails)
            {
                BorrowDetailsVM borrowDetail = new BorrowDetailsVM()
                {
                    Id = bookBorrow.Id,
                    BookName = bookBorrow.Name,
                    BookId = bookBorrow.BookId,
                    UserId = bookBorrow.UserId
                };

                borrowDetails.Add(borrowDetail);
            }

            return borrowDetails;
        }
    }
}
