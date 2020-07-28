using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eLib.Core.Contracts
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> Get();

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression);

        public void Add(T t);

        public void Update(T t);

        public void Delete(T t);

        public void SetUoW(IUoW uow);

        public void SaveChanges();
    }
}
