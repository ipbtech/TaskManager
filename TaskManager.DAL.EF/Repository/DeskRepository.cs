using TaskManager.Dal;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;

namespace TaskManager.DAL.EF.Repository
{
    public class DeskRepository : IRepository<Desk>
    {
        private readonly AppDbContext _db;

        public DeskRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Desk> GetAll()
        {
            return _db.Desks;
        }

        public async Task Create(Desk entity)
        {
            await _db.Desks.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Desk> Update(Desk entity)
        {
            _db.Desks.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Desk entity)
        {
            _db.Desks.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
