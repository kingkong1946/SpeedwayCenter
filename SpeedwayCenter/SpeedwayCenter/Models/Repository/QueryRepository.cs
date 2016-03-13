using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Models.Repository
{
    public class QueryRepository<C, T> : IQueryRepository<T>/*, IDisposable */
        where T: class
        where C: DbContext
    {
        private readonly C _context;
        private bool _disposed;

        public QueryRepository(C context)
        {
            _context = context;
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> FindMany(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).Select(x => x);
        }

        //public void Dispose()
        //{
        //    _disposed = true;
        //    _context.Dispose();
        //}

        //~QueryRepository()
        //{
        //    if (!_disposed)
        //    {
        //        Dispose();
        //    }
        //}
    }
}