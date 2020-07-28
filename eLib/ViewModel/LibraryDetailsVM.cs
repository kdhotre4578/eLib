using eLib.Core.Models;
using System.Collections.Generic;


namespace eLib.ViewModel
{
    public class LibraryDetailsVM
    {
        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<BorrowDetailsVM> BookBorrows { get; set; }
    }
}
