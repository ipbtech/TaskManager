using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Dto
{
    public class Desk : AbstractModel
    {
        public bool IsPrivate { get; set; }
        public string? Columns { get; set; }
        public User? AdminDesk { get; set; }
    }
}
