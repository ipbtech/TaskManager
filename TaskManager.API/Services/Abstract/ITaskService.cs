using TaskManager.Api.Helpers;
using TaskManager.DTO.Task;

namespace TaskManager.Api.Services
{
    public interface ITaskService
    {
        public Task<BaseResponce<bool>> Create(TaskCreateDto createDto, string username);
        public Task<BaseResponce<TaskBaseDto>> Update(int id, TaskUpdateDto updateDto);
        public Task<BaseResponce<bool>> Delete(int id);
        public Task<BaseResponce<TaskBaseDto>> Get(int id);
        public Task<BaseResponce<IEnumerable<TaskBaseDto>>> GetByDesk(int deskId);
        public Task<BaseResponce<IEnumerable<TaskBaseDto>>> GetAssigningUser(string username);
        public Task<BaseResponce<IEnumerable<TaskBaseDto>>> GetCreatingUser(string username);
    }
}
