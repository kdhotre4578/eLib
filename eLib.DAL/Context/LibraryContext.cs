using Microsoft.EntityFrameworkCore;
using eLib.Core.Models;
using eLib.DAL.Configurations;

namespace eLib.DAL.Context
{
    public class LibraryContext : DbContext
    {
        readonly string _connectionString;

        public LibraryContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LibraryContext(DbContextOptions options): base(options)
        { 
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookBorrower> BookBorrows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookBorrowConfig());
        }
    }
}
