namespace TaskManager.DAL.Models
{
    public class Project : AbstractModel
    {
        public int AdminId { get; set; }
        public User? Admin { get; set; }

        public StatusCode Status { get; set; }
        
        public List<UserProjectLink> ProjectUsers { get; set; } = new List<UserProjectLink>();
        public List<Desk> Desks { get; set; } = new List<Desk>();

        public Project() : base() 
        {
            Status = StatusCode.New;
        }
    }
}
