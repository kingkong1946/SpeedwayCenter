using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Models.Repository
{
    public class RiderRepository<T> : EFGenericRepository<Rider, T> where T : DbContext, new()
    {
        public RiderRepository(T context) : base(context)
        {
        }

        public RiderRepository() : this(new T())
        {
        }
        
        public void Delete(int i)
        {
            var entity = FindBy(rider => rider.Id == i);
            Delete(entity.FirstOrDefault());
        }
    }
}