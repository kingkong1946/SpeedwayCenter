using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Models.Repository
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly SpeedwayContext _context;
        private bool _disposed = false;

        public Repository(SpeedwayContext context)
        {
            _context = context;
        }
        
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _disposed = true;
            _context.Dispose();
        }

        ~Repository()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }
    }
}