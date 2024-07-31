using TaskManager.DTO.Project;
using TaskManager.DTO.Task;

namespace TaskManager.DTO.Desk
{
    public  class DeskGetDto : DeskBaseDto
    {
        public int Id { get; set; }
        public ProjectGetShortDto Project { get; set; }
        public List<TaskGetShortDto> Tasks { get; set; }
    }

    public class DeskGetShortDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
