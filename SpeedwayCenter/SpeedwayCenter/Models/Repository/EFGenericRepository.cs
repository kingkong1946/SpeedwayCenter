using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayCenter.Models.Repository
{
    public abstract class EFGenericRepository<C, T> : IRepository<C>, IDisposable
        where T : DbContext, new()
        where C : class 
    {
        private bool _disposed;
        protected T _context;

        protected EFGenericRepository(T context)
        {
            _context = context;
        }

        protected EFGenericRepository() : this(new T())
        {
        }

        public void Dispose()
        {
            _context.Dispose();
            _disposed = true;
            _context = null;
        }

        ~EFGenericRepository()
        {
            if (_disposed)
            {
                _context.Dispose();
            }
        }

        public virtual IQueryable<C> GetAll()
        {
            return _context.Set<C>();
        }

        public virtual IQueryable<C> FindBy(Expression<Func<C, bool>> predicate)
        {
            return _context.Set<C>().Where(predicate).Select(c => c);
        }

        public virtual void Add(C entity)
        {
            _context.Set<C>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
        }

        public virtual void Delete(C entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can't be null.");
            }
            _context.Set<C>().Remove(entity);
        }

        public virtual void Edit(C entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
