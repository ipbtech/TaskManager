using TaskManager.Dto.Helper;

namespace TaskManager.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegistrDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[]? Avatar { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Desk> Desks { get; set; } = new List<Desk>();
        public List<Task> Tasks { get; set; } = new List<Task>();


        public User() { }

        public User(string name, string surname, string email, string password, 
                    UserRole role = UserRole.Viewer, string? phone = null, byte[]? avatar = null)
        {
            FirstName = name;
            LastName = surname;
            Email = email;
            Password = password.HashSha256();
            Role = role;
            Phone = phone;
            Avatar = avatar;

            RegistrDate = DateTime.Now;
        }
    }
}
