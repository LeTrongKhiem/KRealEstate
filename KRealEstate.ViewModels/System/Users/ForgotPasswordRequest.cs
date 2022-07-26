using System.ComponentModel.DataAnnotations;

namespace KRealEstate.ViewModels.System.Users
{
    public class ForgotPasswordRequest
    {
        [Display(Name = "Email đăng ký")]
        public string Email { get; set; }
    }
}
