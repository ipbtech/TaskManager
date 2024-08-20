using TaskManager.Api.Helpers;
using TaskManager.DTO.Desk;

namespace TaskManager.Api.Services
{
    public interface IDeskService
    {
        public Task<BaseResponce<bool>> Create(DeskCreateDto entity, string username);
        public Task<BaseResponce<bool>> Delete(int id, string username);
        public Task<BaseResponce<DeskBaseDto>> Update(int id, DeskUpdateDto entity, string username);
        public Task<BaseResponce<DeskBaseDto>> Get(int id);
        public Task<BaseResponce<IEnumerable<DeskBaseDto>>> GetByProjectId(int projectId);
    }
}
