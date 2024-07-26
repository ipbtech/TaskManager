﻿namespace TaskManager.DAL.Models
{
    public class WorkTask : AbstractModel
    {
        public int? DeskId { get; set; }
        public Desk? Desk { get; set; }
        
        public string? ColumnOfDesk { get; set; }
        public byte[]? AttachmentsData { get; set; }

        public int? CreatorId { get; set; }
        public User? Creator { get; set; }

        public int? ContractorId { get; set; }
        public User? Contractor { get; set; }

        public WorkTask() : base() { }
    }
}