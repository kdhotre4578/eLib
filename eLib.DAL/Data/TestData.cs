using eLib.DAL.Context;
using eLib.Core.Models;
using System.Collections.Generic;

namespace eLib.DAL.Data
{
    public class TestData
    {
        public void SeedData(LibraryContext _context)
        {
            List<Book> books = new List<Book>();

            books.Add(new Book() { Name = "Five Point Someone", Quantity = 2 });
            books.Add(new Book() { Name = "Harry Potter", Quantity = 1 });
            books.Add(new Book() { Name = "Monk Who Sold His Ferrari", Quantity = 10 });
            books.Add(new Book() { Name = "Rich Dad Poor Dad", Quantity = 10});

            _context.Books.AddRange(books);
            _context.SaveChanges();
        }
    }
}
