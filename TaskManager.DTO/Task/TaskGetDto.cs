namespace TaskManager.DTO.Task
{
    public class TaskGetDto : TaskBaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatorId { get; set; }
        public int DeskId { get; set; }
    }

    public class TaskGetShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
