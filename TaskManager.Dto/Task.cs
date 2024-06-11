using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Dto
{
    public class Task : AbstractModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User? Creator { get; set; }
        public User? Contractor { get; set; }
        public byte[]? AttachmentsData { get; set; }
    }
}
