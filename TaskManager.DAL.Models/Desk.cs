namespace TaskManager.DAL.Models
{
    public class Desk : AbstractModel
    {
        public string? DeskColumns { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();

        public Desk() : base() { }
    }
}
