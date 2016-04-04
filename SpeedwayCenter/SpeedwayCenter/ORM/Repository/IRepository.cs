namespace SpeedwayCenter.ORM.Repository
{
    public interface IRepository<T> : IQueryRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
