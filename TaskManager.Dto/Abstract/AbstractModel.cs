using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Dto
{
    public abstract class AbstractModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[]? Image { get; set; }

        public AbstractModel()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
