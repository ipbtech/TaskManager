namespace TaskManager.Api.Services
{
    public interface IService<T> where T : class
    {
        public Task<bool> Create(T entity);
    }
}
