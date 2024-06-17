namespace TaskManager.DAL.Models
{
    public class Desk : AbstractModel
    {
        public bool IsPrivate { get; set; }
        public string? DeskColumns { get; set; }

        public int DeskOwnerId { get; set; }
        public User? DeskOwner { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();

        public Desk() : base() { }
    }
}
