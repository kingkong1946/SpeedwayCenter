using System.Linq;

namespace SpeedwayCenter.ORM
{
    public interface IDatabaseContext
    {
        IQueryable<T> Get<T>() where T : class;
        T Add<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Save();
    }
}