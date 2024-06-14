namespace TaskManager.Dto
{
    public class Task : AbstractModel
    {
        public int DeskId { get; set; }
        public Desk Desk { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Column { get; set; }
        public byte[]? AttachmentsData { get; set; }

        public int CreatorId { get; set; }
        //public User? Creator { get; set; }

        public int? ContractorId { get; set; }
        //public User? Contractor { get; set; }
    }
}
