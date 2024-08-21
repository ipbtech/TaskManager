namespace TaskManager.DTO.Desk.ColumnDesk
{
    public class AddingColumnDto
    {
        public int DeskId { get; set; }
        public string Name { get; set; }
    }

    public class UpdatingColumnDto
    {
        public int DeskId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
    }

    public class  DeletingColumnDto : AddingColumnDto
    { }
}
