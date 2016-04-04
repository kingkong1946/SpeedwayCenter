using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpeedwayCenter.ORM.Repository
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        public readonly IDatabaseContext _context;

        public QueryRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Get<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Get<T>();
        }

        public IQueryable<T> FindMany(Expression<Func<T, bool>> predicate)
        {
            return _context.Get<T>().Where(predicate).Select(x => x);
        }
    }
}