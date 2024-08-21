using TaskManager.Api.Helpers;
using TaskManager.DTO.Desk;
using TaskManager.DTO.Desk.ColumnDesk;

namespace TaskManager.Api.Services
{
    public interface IDeskService
    {
        public Task<BaseResponce<bool>> Create(DeskCreateDto entity, string username);
        public Task<BaseResponce<bool>> Delete(int id, string username);
        public Task<BaseResponce<DeskBaseDto>> Update(int id, DeskUpdateDto entity, string username);
        public Task<BaseResponce<DeskBaseDto>> Get(int id);
        public Task<BaseResponce<IEnumerable<DeskBaseDto>>> GetByProjectId(int projectId);

        public Task<BaseResponce<bool>> AddColumnDesk(AddingColumnDto entity, string username);
        public Task<BaseResponce<bool>> UpdateColumnDesk(UpdatingColumnDto entity, string username);
        public Task<BaseResponce<bool>> DeleteColumnDesk(DeletingColumnDto entity, string username);
    }
}
