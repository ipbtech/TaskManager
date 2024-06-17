using Microsoft.EntityFrameworkCore;
using TaskManager.Dal.Config;
using TaskManager.DAL.Models;

namespace TaskManager.Dal
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.ApplyConfiguration(new UserDbConfig());

            modelBuilder.Entity<Project>().ToTable("projects");
            modelBuilder.ApplyConfiguration(new ProjectDbConfig());

            modelBuilder.Entity<Desk>().ToTable("desks");
            modelBuilder.ApplyConfiguration(new DeskDbConfig());

            modelBuilder.Entity<WorkTask>().ToTable("tasks");
            modelBuilder.ApplyConfiguration(new TaskDbConfig());

            modelBuilder.Entity<UserProjectLink>().ToTable("users_projects");
            modelBuilder.ApplyConfiguration(new UserProjectLinkDbConfig());
        }
    }
}
