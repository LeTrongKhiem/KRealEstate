namespace KRealEstate.ViewModels.System.Users
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberPassword { get; set; }
    }
}
