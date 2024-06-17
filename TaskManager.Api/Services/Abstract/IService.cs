using TaskManager.Api.Dto.Base;

namespace TaskManager.Api.Services
{
    public interface IService<T> where T : class
    {
        public Task<bool> Create(T entity);
        public Task<bool> Delete(int id);
        public Task<BaseResponce<T>> Update(int id, T entity);
        public Task<BaseResponce<T>> Get(int id);
        public Task<BaseResponce<List<T>>> GetAll();
    }
}
