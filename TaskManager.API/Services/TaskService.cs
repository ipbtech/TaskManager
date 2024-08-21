using TaskManager.Api.Helpers;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;
using TaskManager.DTO.Task;

namespace TaskManager.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<WorkTask> _taskRepo;

        public TaskService(IRepository<WorkTask> taskRepo)
        {
            _taskRepo = taskRepo;
        }
        
        
        public Task<BaseResponce<bool>> Create(TaskCreateDto createDto)
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

        public Task<BaseResponce<TaskBaseDto>> GetByDesk(int deskId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<TaskBaseDto>> GetByProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<TaskBaseDto>> GetByUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<TaskBaseDto>> Update(int id, TaskUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
