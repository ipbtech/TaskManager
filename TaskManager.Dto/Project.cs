namespace TaskManager.Dto
{
    public class Project : AbstractModel
    {
        public int AdminId { get; set; }
        //public User Admin { get; set; }

        public StatusCode Status { get; set; }
        
        public List<User> Users { get; set; } = new List<User>();
        public List<Desk> Desks { get; set; } = new List<Desk>();
    }
}
