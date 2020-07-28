using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eLib.Models;
using eLib.Core.Contracts;
using Microsoft.AspNetCore.Http;
using eLib.ViewModel;
using eLib.Helper;

namespace eLib.Controllers
{
    public class HomeController : Controller
    {
        private const string BORROW_STATUS = "BorrowStatus";
        private const int userId = 1;

        private readonly ILibrary _libraryService;

        public HomeController(ILibrary libraryService)
        {
            _libraryService = libraryService;
        }

        public IActionResult Index()
        {
            ViewData[BORROW_STATUS] = (TempData != null && TempData[BORROW_STATUS] != null) ? TempData[BORROW_STATUS] : null;
            return View(GetLibraryDetails());
        }

        public IActionResult BorrowBook(int bookId)
        {
            if(Verify(userId, bookId))
            {
                _libraryService.BorrowBook(bookId, 1);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ReturnBook(int bookId)
        {
            _libraryService.SubmitBook(bookId, userId);

            return RedirectToAction(nameof(Index));
        }

        private LibraryDetailsVM GetLibraryDetails()
        {
            var books = _libraryService.GetBooksList();
            var borrowedBookDetails = new HelperMethods().GetBorrowBookDetails(books, _libraryService.GetBorrowedBookList(userId));

            return new LibraryDetailsVM()
            {
                Books = books,
                BookBorrows = borrowedBookDetails
            };
        }

        private bool Verify(int userId, int bookId)
        {
            TempData[BORROW_STATUS] = null;

            if (!_libraryService.CanBorrow(userId))
            {
                TempData[BORROW_STATUS] = "Already borrowed two books";
                return false;
            }
            else if (_libraryService.IsAlreadyBorrowed(userId, bookId))
            {
                TempData[BORROW_STATUS] = "Already borrowed";
                return false;
            }
            
            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
