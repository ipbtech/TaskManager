namespace TaskManager.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string HashPassword { get; set; }
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegistrDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public List<Project> AdminProjects { get; set; } = new List<Project>();
        public List<UserProjectLink> UserProjects { get; set; } = new List<UserProjectLink>();
        public List<Desk> Desks { get; set; } = new List<Desk>();
        public List<WorkTask> AssigningTasks { get; set; } = new List<WorkTask>();
        public List<WorkTask> CreatingTasks { get; set; } = new List<WorkTask>();


        public User() 
        {
            RegistrDate = DateTime.Now;
        }
    }
}
