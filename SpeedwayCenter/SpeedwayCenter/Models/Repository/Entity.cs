using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Models.Repository
{
    public abstract class Entity<C, T> : IRepository<C>, IDisposable
        where T : DbContext, new()
        where C : class 
    {
        private bool _disposed;
        protected T _context;
        protected DbSet<C> Table;

        protected Entity(T context)
        {
            _context = context;
        }

        protected Entity() : this(new T())
        {
        }

        public void Dispose()
        {
            _context.Dispose();
            _disposed = true;
            _context = null;
        }

        ~Entity()
        {
            if (_disposed)
            {
                _context.Dispose();
            }
        }

        public IQueryable<C> GetAll()
        {
            return _context.Set<C>();
        }

        public IQueryable<C> FindBy(Expression<Func<C, bool>> predicate)
        {
            return _context.Set<C>().Where(predicate).Select(c => c);
        }

        public void Add(C entity)
        {
            _context.Set<C>().Add(entity);
        }

        public void Delete(C entity)
        {
            _context.Set<C>().Remove(entity);
        }

        public void Edit(C entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
