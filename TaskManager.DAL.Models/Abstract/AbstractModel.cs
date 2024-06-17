namespace TaskManager.DAL.Models
{
    public abstract class AbstractModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte[]? Image { get; set; }

        public AbstractModel()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
