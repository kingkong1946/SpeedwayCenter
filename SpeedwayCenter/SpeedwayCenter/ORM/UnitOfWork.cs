using System.Collections.Generic;
using System.Linq;
using SpeedwayCenter.ORM.Repository;

namespace SpeedwayCenter.ORM
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _context;
        private readonly IList<object> _list;

        public UnitOfWork(IDatabaseContext context, IList<object> list)
        {
            _context = context;
            _list = list;
        }

        public IQueryRepository<T> GetQueryRepository<T>() where T : class
        {
            if (!_list.Any(o => o is IQueryRepository<T>))
            {
                _list.Add(new QueryRepository<T>(_context));
            }
            return _list.FirstOrDefault(o => o is IQueryRepository<T>) as IQueryRepository<T>;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!_list.Any(o => o is IRepository<T>))
            {
                _list.Add(new Repository<T>(_context));
            }
            return _list.FirstOrDefault(o => o is IRepository<T>) as IRepository<T>;
        }

        public void Save()
        {
            _context.Save();
        }
    }
}