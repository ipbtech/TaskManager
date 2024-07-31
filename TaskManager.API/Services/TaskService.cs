using TaskManager.API.Helpers;
using TaskManager.DTO.Task;

namespace TaskManager.API.Services
{
    public class TaskService : ITaskService
    {
        public Task<BaseResponce<bool>> Create(TaskBaseDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<TaskBaseDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<IEnumerable<TaskBaseDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<TaskBaseDto>> Update(int id, TaskBaseDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
