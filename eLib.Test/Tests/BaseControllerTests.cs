using eLib.DAL.Context;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace eLib.Test
{
    [TestFixture]
    public abstract class BaseControllerTests
    {
        protected LibraryContext _context;

        [SetUp]
        public void Initialise()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new LibraryContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Release()
        {
            _context.Database.EnsureDeleted();
            _context = null;
        }
    }
}
