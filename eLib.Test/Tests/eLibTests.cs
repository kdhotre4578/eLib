using eLib.BLayer;
using eLib.Controllers;
using eLib.DAL.Data;
using eLib.DAL.Models;
using eLib.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace eLib.Test
{
    [TestFixture]
    public class eLibTests : BaseControllerTests
    {
        [Test]
        public void Get_NoBookPresent_Returns_ZeroCount()
        {
            HomeController homeController = GetHomeController();

            var result = homeController.Index();
            var viewResult = result as ViewResult;
            var model = viewResult.Model as LibraryDetailsVM;
            
            Assert.AreEqual(0, model.Books.Count());
        }

        [Test]
        public void Get_SeededBooks_Returns_BookCount()
        {
            HomeController homeController = GetHomeController();
            new TestData().SeedData(_context);

            var result = homeController.Index();
            var viewResult = result as ViewResult;
            var model = viewResult.Model as LibraryDetailsVM;

            Assert.AreEqual(4, model.Books.Count());
        }

        [Test]
        public void Get_SeededBooks_BorrowBook_Returns_BookCount()
        {
            HomeController homeController = GetHomeController();
            new TestData().SeedData(_context);

            homeController.BorrowBook(1);

            var result = homeController.Index();
            var viewResult = result as ViewResult;
            var model = viewResult.Model as LibraryDetailsVM;

            Assert.AreEqual(1, model.Books.FirstOrDefault(x => x.Id == 1).Quantity);
            Assert.AreEqual(1, model.BookBorrows.Count());
        }

        [Test]
        public void Get_SeededBooks_BorrowNReturnBook_Returns_BookCount()
        {
            HomeController homeController = GetHomeController();
            new TestData().SeedData(_context);

            homeController.BorrowBook(1);
            homeController.ReturnBook(1);

            var result = homeController.Index();
            var viewResult = result as ViewResult;
            var model = viewResult.Model as LibraryDetailsVM;

            Assert.AreEqual(2, model.Books.FirstOrDefault(x => x.Id == 1).Quantity);
            Assert.AreEqual(0, model.BookBorrows.Count());
        }

        [Test]
        public void Get_SeededBooks_BorrowBookTwice_Returns_AlreadyBorrowedMessage()
        {
            HomeController homeController = GetHomeController();
            new TestData().SeedData(_context);

            homeController.BorrowBook(1);
            homeController.BorrowBook(1);

            var result = homeController.Index();
            var viewResult = result as ViewResult;
            
            Assert.AreEqual("Already borrowed", viewResult.TempData["BorrowStatus"]);
        }

        [Test]
        public void Get_SeededBooks_BorrowDiffBookThrice_Returns_AlreadyBorrowedTwoBookMessage()
        {
            HomeController homeController = GetHomeController();
            new TestData().SeedData(_context);

            homeController.BorrowBook(1);
            homeController.BorrowBook(2);
            homeController.BorrowBook(3);

            var result = homeController.Index();
            var viewResult = result as ViewResult;

            Assert.AreEqual("Already borrowed two books", viewResult.TempData["BorrowStatus"]);
        }

        public HomeController GetHomeController()
        {
            return new HomeController(new LibraryService(new UoW(_context)))
            {
                TempData = GetTempDataMoq()
            };
        }

        private ITempDataDictionary GetTempDataMoq()
        {
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            return tempData;
        }
    }
}
