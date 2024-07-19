using TaskManager.API.Helpers;

namespace TaskManager.API.Services
{
    public interface IService<T> where T : class
    {
        public Task<BaseResponce<bool>> Create(T entity);
        public Task<BaseResponce<bool>> Delete(int id);
        public Task<BaseResponce<T>> Update(int id, T entity);
        public Task<BaseResponce<T>> Get(int id);
        public Task<BaseResponce<IEnumerable<T>>> GetAll();
    }
}
