using TaskManager.Api.Helpers;
using TaskManager.DTO.Task;

namespace TaskManager.Api.Services
{
    public interface ITaskService
    {
        public Task<BaseResponce<bool>> Create(TaskCreateDto createDto);
        public Task<BaseResponce<TaskBaseDto>> Update(int id, TaskUpdateDto updateDto);
        public Task<BaseResponce<bool>> Delete(int id);
        public Task<BaseResponce<TaskBaseDto>> Get(int id);
        public Task<BaseResponce<TaskBaseDto>> GetByProject(int projectId);
        public Task<BaseResponce<TaskBaseDto>> GetByDesk(int deskId);
        public Task<BaseResponce<TaskBaseDto>> GetByUser(string username);
    }
}
