using eLib.Core.Contracts;
using eLib.DAL.Context;
using eLib.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eLib.DAL.Models
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private LibraryContext _libDbContext;

        public Repository(LibraryContext libContext)
        {
             _libDbContext = libContext;

            if (_libDbContext.Database.EnsureCreated())
            {
                new TestData().SeedData(_libDbContext);
            }
        }

        public Repository(string connectionString)
        {
            _libDbContext = new LibraryContext(connectionString);

            if (_libDbContext.Database.EnsureCreated())
            {
                new TestData().SeedData(_libDbContext);
            }
        }

        public void Add(T t)
        {
            if (t != null)
            {
                _libDbContext.Set<T>().Add(t);
            }
        }

        public IEnumerable<T> Get()
        {
            return _libDbContext.Set<T>().AsQueryable<T>().ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _libDbContext.Set<T>().AsQueryable<T>().Where(expression);
        }

        public void Update(T t)
        {
            if (t != null)
            {
                _libDbContext.Set<T>().Update(t);
            }
        }

        public void Delete(T t)
        {
            if (t != null)
            {
                _libDbContext.Set<T>().Remove(t);
            }
        }

        public void SetUoW(IUoW uow)
        {
            _libDbContext = ((UoW)uow).LibDbContext;
        }

        public void SaveChanges()
        {
            _libDbContext.SaveChangesAsync();
        }
    }
}
