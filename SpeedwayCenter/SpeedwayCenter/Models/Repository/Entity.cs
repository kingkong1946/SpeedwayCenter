using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayCenter.Models.Repository
{
    public abstract class Entity<T> : IDisposable
        where T : DbContext, new()
    {
        protected bool _disposed;
        protected T _context = new T();

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
    }
}
