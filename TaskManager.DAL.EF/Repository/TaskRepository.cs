using TaskManager.Dal;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;

namespace TaskManager.DAL.EF.Repository
{
    public class TaskRepository : IRepository<WorkTask>
    {
        private readonly AppDbContext _db;

        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<WorkTask> GetAll()
        {
            return _db.Tasks;
        }

        public async Task Create(WorkTask entity)
        {
            await _db.Tasks.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<WorkTask> Update(WorkTask entity)
        {
            _db.Tasks.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(WorkTask entity)
        {
            _db.Tasks.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
