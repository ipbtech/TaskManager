namespace TaskManager.Dto
{
    public class Desk : AbstractModel
    {
        public bool IsPrivate { get; set; }
        public string? Columns { get; set; }

        public int AdminId { get; set; }
        //public User Admin { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public List<Task> Tasks = new List<Task>();
    }
}
