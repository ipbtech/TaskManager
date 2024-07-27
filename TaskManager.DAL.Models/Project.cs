namespace TaskManager.DAL.Models
{
    public class Project : AbstractModel
    {
        public int? AdminId { get; set; }
        public User? Admin { get; set; }

        public StatusCode Status { get; set; }
        
        public List<User> ProjectUsers { get; set; } = new List<User>();
        public List<Desk> Desks { get; set; } = new List<Desk>();

        public Project() : base() 
        {
            Status = StatusCode.New;
        }
    }
}
