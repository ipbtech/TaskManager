using TaskManager.API.Helpers;
using TaskManager.DTO.Desk;

namespace TaskManager.API.Services
{
    public class DeskService : IService<DeskBaseDto>
    {
        public Task<BaseResponce<bool>> Create(DeskBaseDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<DeskBaseDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<IEnumerable<DeskBaseDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponce<DeskBaseDto>> Update(int id, DeskBaseDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
