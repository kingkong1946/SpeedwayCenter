using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SpeedwayCenter.Models.Repository
{
    public class QueryRepository<C, T> : IQueryRepository<T>
        where T: class
        where C: DbContext
    {
        private readonly C _context;

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
    }
}