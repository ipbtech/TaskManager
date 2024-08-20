using TaskManager.Api.Helpers;
using TaskManager.DTO.Project;

namespace TaskManager.Api.Services
{
    public interface IProjectService 
    {
        public Task<BaseResponce<IEnumerable<ProjectBaseDto>>> GetAll(string username);
        public Task<BaseResponce<ProjectBaseDto>> Get(int id);
        public Task<BaseResponce<bool>> Create(ProjectCreateDto entity);
        public Task<BaseResponce<bool>> Delete(int id);
        public Task<BaseResponce<ProjectBaseDto>> Update(int id, ProjectUpdateDto entity);   
        public Task<BaseResponce<bool>> AddUsersToProject(int projectId, List<int> userIds);
        public Task<BaseResponce<bool>> RemoveUsersFromProject(int projectId, List<int> userIds);
    }
}
