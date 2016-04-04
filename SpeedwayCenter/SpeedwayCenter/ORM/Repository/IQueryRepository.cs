using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpeedwayCenter.ORM.Repository
{
    public interface IQueryRepository<T> where T: class
    {
        T FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> FindMany(Expression<Func<T, bool>> predicate);
    }
}
