namespace TaskManager.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[]? Avatar { get; set; }
    }
}
