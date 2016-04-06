using SpeedwayCenter.ORM.Repository;

namespace SpeedwayCenter.ORM
{
    public interface IUnitOfWork
    {
        IQueryRepository<T> GetQueryRepository<T>() where T : class;
        IRepository<T> GetRepository<T>() where T : class;

        void Save();
    }
}