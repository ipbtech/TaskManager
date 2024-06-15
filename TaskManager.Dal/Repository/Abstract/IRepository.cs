namespace TaskManager.Dal.Repository
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        public Task Create(T entity);
        public Task<T> Update(T entity);
        public Task Delete(T entity);
    }
}
