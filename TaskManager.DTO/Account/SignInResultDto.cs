using TaskManager.DTO.User;

namespace TaskManager.DTO.Account
{
    public class SignInResultDto
    {
        public string AccessToken { get; set; }
        public UserBaseDto User { get; set; }
    }
}
