using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DTO.Task
{
    public class TaskGetDto : TaskBaseDto
    {

    }

    public class TaskGetShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
