using TaskManager.DAL.Models;

namespace TaskManager.Dal.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _db;
        
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public async Task Create(User entity)
        {
            var z = await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
