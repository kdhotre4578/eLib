using eLib.Core.Contracts;
using eLib.Core.Models;
using eLib.DAL.Context;
using System;

namespace eLib.DAL.Models
{
    public class UoW : IUoW
    {
        private IRepository<Book> _bookRepository;

        private IRepository<BookBorrower> _bookBorrowerRepository;

        public LibraryContext LibDbContext { get; }

        public IRepository<Book> BookRepository => _bookRepository ??= new Repository<Book>(LibDbContext);

        public IRepository<BookBorrower> BookBorrowerRepository => _bookBorrowerRepository ??= new Repository<BookBorrower>(LibDbContext);

        public UoW(string connectionString)
        {
            LibDbContext = new LibraryContext(connectionString);
        }

        public UoW(LibraryContext libDbContext)
        {
            LibDbContext = libDbContext;
        }

        public void Commit()
        {
            LibDbContext.SaveChanges();
        }
    }
}
