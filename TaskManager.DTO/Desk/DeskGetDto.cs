using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.Desk
{
    public  class DeskGetDto : DeskBaseDto
    {

    }

    public class DeskShortGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
