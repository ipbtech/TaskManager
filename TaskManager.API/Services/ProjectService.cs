using TaskManager.API.Helpers;
using TaskManager.DAL.Models;
using TaskManager.DTO.Project;

namespace TaskManager.API.Services
{
    public class ProjectService : IService<ProjectBaseDto>
    {
        public Task<BaseResponce<bool>> Create(ProjectBaseDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<ProjectBaseDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<IEnumerable<ProjectBaseDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<ProjectBaseDto>> Update(int id, ProjectBaseDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
