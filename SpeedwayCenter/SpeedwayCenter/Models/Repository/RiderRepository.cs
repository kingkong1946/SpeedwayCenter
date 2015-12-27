using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Models.Repository
{
    public class RiderRepository : Entity<SpeedwayEntities>, IRepository<Rider>
    {
        public IQueryable<Rider> GetAll()
        {
            return _context.Riders;
        }

        public Rider Find(int Id)
        {
            return _context.Riders.FirstOrDefault(rider => rider.Id == Id);
        }

        public IQueryable<Rider> FindBy(Expression<Func<Rider, bool>> predicate)
        {
            return _context.Riders.Where(predicate).Select(rider => rider);
        }

        public void Add(Rider entity)
        {
            _context.Riders.Add(entity);
        }

        public void Delete(Rider entity)
        {
            _context.Riders.Remove(entity);
        }

        public void Edit(Rider entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}