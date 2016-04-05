namespace SpeedwayCenter.ORM.Repository
{
    public class Repository<T> : QueryRepository<T>, IRepository<T> where T : class
    {
        public Repository(IDatabaseContext context) : base(context)
        {
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Delete(entity);
        }

        public void Edit(T entity)
        {
            _context.Update(entity);
        }

        public void Save()
        {
            _context.Save();
        }
    }
}