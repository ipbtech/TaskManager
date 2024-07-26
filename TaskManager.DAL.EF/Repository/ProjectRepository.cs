using TaskManager.Dal;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;

namespace TaskManager.DAL.EF.Repository
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly AppDbContext _db;

        public ProjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Project> GetAll()
        {
            return _db.Projects;
        }

        public async Task Create(Project entity)
        {
            await _db.Projects.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Project> Update(Project entity)
        {
            _db.Projects.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Project entity)
        {
            _db.Projects.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
